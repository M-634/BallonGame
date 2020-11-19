using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
/// <summary>
/// デフォルト状態のユーザー制御によるプレイヤーの動きを制御する
/// ギミック等を受けた場合のプレイヤーが特殊な状態にある場合は別クラスに操作方法を書く
/// </summary>
public class PlayerBaseMove : MonoBehaviour
{
    /// <summary>rigidbodyを格納する変数 </summary>
    public static Rigidbody m_rb;
    /// <summary>全身する力。RigidBodyのAddForceで制御する </summary>
    public float m_forwardForce = 100;
    /// <summary>空気抵抗の比率 </summary>
    [SerializeField, Range(0, 1f)] public float m_airBrekeCoefficient = 0.995f;
    /// <summary>回転の減衰比率 </summary>
    [SerializeField, Range(0, 1f)] public float m_rotateBrekeCoefficient = 0.9f;
    /// <summary>横向いた時の追加速度の減衰比率 </summary>
    [SerializeField, Range(0, 1f)] public float m_addAirBrake = 0.7f;
    /// <summary>横向いた時の追加速度の減衰比率の角度(度数法) </summary>
    [SerializeField] public float m_addAirBrakeStart = 1f;
    /// <summary>速度を格納する。現状DebugUIに値を渡してる </summary>
    public float mp_Speed { get; private set; }
    /// <summary>速度制限をする。最大速度 </summary>
    public float m_maxSpeed = 50;
    /// <summary>最大速度を超過したときにかかるブレーキの係数 </summary>
    [SerializeField, Range(0, 1f)] public float m_maxSpeedExcessBrake = 0.9f;
    /// <summary>スワイプした時にどの程度指に付いてくるかの係数 </summary>
    [SerializeField] public float m_horizontalSpeed = 0.5f;

    /// <summary>touchを格納、画面タッチをしてる一本目の指を取得する。現状指一本 </summary>
    Touch m_touch;
    /// <summary>x軸のスワイプの動きを格納する</summary>
    public float m_swipeDistance_x = 0;
    /// <summary>y軸のスワイプの動きを格納する</summary>
    public float m_swipeDistance_y = 0;
    /// <summary>スワイプした距離の最大値</summary>
    public float m_maxSwipeDistance_y = 0.2f;
    /// <summary>ブレーキがかかるスワイプ距離</summary>
    public float m_beginSwipeBrake = 0.05f;

    //[SerializeField] GameObject m_debugUIobj;

    /// <summary>unity上でマウスを使ってデバッグを行う時にフラグをオンにする </summary>
    [SerializeField] bool m_mouthDebug;
    Vector3 m_mouthPosi;

    /// <summary>スワイプをしたかどうかのフラグ。回転力を加えるとき一回だけrotateForceに+=をしたい </summary>
    public bool m_onSwipe = false;
    /// <summary> プレイヤーの回転速度</summary>
    public float mp_RotateSpeed { get; private set; }

    /// <summary>進行方向とみてる方向の角度 </summary>
    public float m_forwardToLookAngle = 0;

    /// <summary>調整する高さの位置。この値から一定値を超えてズレたらこの高さにプレイヤーの高さを調整する </summary>
    Vector3 m_AdjustHeightPosition;

    [SerializeField] PlayerEventHandller m_playerEventHandller;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_AdjustHeightPosition = gameObject.transform.position;
    }
    // Update is called once per frame
    private void Update()
    {
        mp_Speed = m_rb.velocity.magnitude;
    }
    void FixedUpdate()
    {
        if (!m_playerEventHandller.InGame) return;


        if (m_mouthDebug)
        {
            SetMouthAim();
            SetMouthForce();
        }
        else
        {
            AddTouchMoveForce();
            GetSwipeDistance();
        }

        SetAirBrake();
        if (m_rb.velocity.magnitude > m_maxSpeed) //現在の速度が最大速度を超えていたら空気抵抗を上げる
        {
            LimitSpeed();
        }
        SetRotateSpeed();

        if (Mathf.Abs(transform.position.y - m_AdjustHeightPosition.y) > 0.01f) //初期位置より一定値を超えて高さがズレていたら位置調整する
        {
            SetHeightAdjust();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        SetProgressAngle();
    }


    /// <summary>加減速を計算する</summary>
    void AddTouchMoveForce()
    {
        if (Input.touchCount > 0)
        {
            m_touch = Input.GetTouch(0);
            if (m_touch.phase == TouchPhase.Ended)
            {
                if (!m_onSwipe) //前のフレームでスワイプしていなかったとき指を離したら加速する。
                {
                    m_rb.AddForce(this.transform.forward * m_forwardForce);
                }
            }
        }
    }


    /// <summary>
    /// スピードの最高速度を設定。超過したら空気抵抗を強める
    /// </summary>
    void LimitSpeed()
    {
        m_rb.velocity = m_rb.velocity * m_maxSpeedExcessBrake;
    }

    /// <summary>
    /// スワイプした距離を測る関数
    /// </summary>
    void GetSwipeDistance()
    {
        // 画面タッチが行われたら
        if (m_touch.phase == TouchPhase.Began)
        {
            //touchBeginPos = touch.position;
            m_swipeDistance_x = 0;
            m_swipeDistance_y = 0;
        }

        if (m_touch.phase == TouchPhase.Moved)
        {
            m_onSwipe = true;

            m_swipeDistance_x = m_touch.deltaPosition.x / Screen.width;
            m_swipeDistance_y = m_touch.deltaPosition.y / Screen.height;
        }
        if (m_touch.phase == TouchPhase.Ended)
        {
            m_onSwipe = false;
        }
    }

    /// <summary>
    /// ユーザーのスワイプした距離をプレイヤーの回転に反映する関数
    /// </summary>
    void SetRotateSpeed()
    {
        //プレイヤーのスワイプ以外での回転を無くす
        if (m_rb.angularVelocity.magnitude > 0)
        {
            m_rb.angularVelocity = m_rb.angularVelocity * 0;
        }

        if (m_onSwipe || m_mouthDebug)
        {
            mp_RotateSpeed = m_horizontalSpeed * m_swipeDistance_x;
        }
        else
        {
            mp_RotateSpeed *= m_rotateBrekeCoefficient;
            if (Mathf.Abs(mp_RotateSpeed) <= 0.01f)
            {
                mp_RotateSpeed = 0;
            }
        }
        transform.Rotate(0, mp_RotateSpeed, 0);
    }

    /// <summary>
    /// 空気抵抗を処理する。
    /// 横を向かせるようにスワイプするとブレーキが強まる。
    /// 一定値未満のスピードだと止まる
    /// </summary>
    void SetAirBrake()
    {
        m_rb.velocity = m_rb.velocity * m_airBrekeCoefficient;
        //if (Mathf.Abs(forwardToLookAngle) > addAirBrakeStart) //横向いた時の追加ブレーキを判定する
        //{
        //    m_rb.velocity = m_rb.velocity * addAirBrake;
        //}

        if (m_swipeDistance_y < -m_beginSwipeBrake)
        {
            m_rb.velocity = m_rb.velocity * m_addAirBrake * GetSwipeYaxisRate();
        }

        if (m_rb.velocity.magnitude < 0.01f)
        {
            m_rb.velocity = m_rb.velocity * 0;
        }
    }

    float GetSwipeYaxisRate()
    {
        float swipeYaxisRate = 0;
        swipeYaxisRate = 1 - Mathf.InverseLerp(m_swipeDistance_y, m_maxSwipeDistance_y, swipeYaxisRate);
        return swipeYaxisRate;
    }

    /// <summary>
    /// 進行方向の角度をx,z回転を0にするように矯正する。
    /// </summary>
    void SetProgressAngle()
    {
        Quaternion nowAngle = transform.rotation;
        nowAngle.x = 0;
        nowAngle.z = 0;
        transform.localRotation = nowAngle;
    }

    /// <summary>
    /// 高さ調整の関数
    /// </summary>
    void SetHeightAdjust()
    {
        m_rb.AddForce(new Vector3(0, m_AdjustHeightPosition.y - transform.position.y, 0));
    }

    /// <summary>
    /// 進行方向とみている方向の角度を計算する
    /// </summary>
    //void SetForwardToLookAngle()
    //{
    //    forwardToLookAngle = Vector3.Angle(m_rb.velocity, transform.forward);
    //}

    /// <summary>
    /// unityで実行してデバッグする用。マウスクリックで加速
    /// </summary>
    void SetMouthForce()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_rb.AddForce(this.transform.forward * m_forwardForce);
        }
        else if (!Input.GetMouseButton(0))
        {
            //m_rb.velocity = m_rb.velocity * coefficient;
        }
    }

    /// <summary>
    /// デバッグ用。プレイヤーがマウスの方を向く
    /// </summary>
    void SetMouthAim()
    {
        m_mouthPosi = Input.mousePosition;
        m_swipeDistance_x = m_mouthPosi.x / Screen.width;
    }
}
