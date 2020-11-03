using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// デフォルトのユーザー制御によるプレイヤーの動きを制御する
/// </summary>
public class PlayerBaseMove : MonoBehaviour
{
    Rigidbody m_rb;
    /// <summary>全身する力。RigidBodyのAddForceで制御する </summary>
    public float forwardForce = 50;
    /// <summary>空気抵抗の比率 </summary>
    [SerializeField, Range(0, 1f)] public float airBrekeCoefficient = 0.995f;
    /// <summary>回転の減衰比率 </summary>
    [SerializeField, Range(0, 1f)] public float rotateBrekeCoefficient = 0.9f;
    /// <summary>横向いた時の追加速度の減衰比率 </summary>
    [SerializeField, Range(0, 1f)] public float addAirBrake = 0.7f;
    /// <summary>横向いた時の追加速度の減衰比率の角度(度数法) </summary>
    [SerializeField] public float addAirBrakeStart = 1f;
    /// <summary>速度を格納する。現状DebugUIに値を渡してる </summary>
    public float Speed { get; private set; }
    /// <summary>速度制限をする。最大速度 </summary>
    public float maxSpeed = 50;
    /// <summary>最大速度を超過したときにかかるブレーキの係数 </summary>
    [SerializeField, Range(0, 1f)] public float maxSpeedExcessBrake = 0.9f;
    /// <summary>スワイプした時にどの程度指に付いてくるかの係数 </summary>
    [SerializeField] public float horizontalSpeed = 0.5f;

    /// <summary>touchを格納、画面タッチをしてる一本目の指を取得する。現状指一本 </summary>
    Touch touch;
    /// <summary>x軸のスワイプの動きを格納する</summary>
    public float swipeDistance_x = 0;
    /// <summary>y軸のスワイプの動きを格納する</summary>
    public float swipeDistance_y = 0;
    /// <summary>スワイプした距離の最大値</summary>
    public float maxSwipeDistance_y = 0.2f;
    /// <summary>ブレーキがかかるスワイプ距離</summary>
    public float beginSwipeBrake = 0.05f;

    [SerializeField] GameObject debugUIobj;

    /// <summary>unity上でマウスを使ってデバッグを行う時にフラグをオンにする </summary>
    [SerializeField] bool mouthDebug;
    Vector3 mouthPosi;

    /// <summary>スワイプをしたかどうかのフラグ。回転力を加えるとき一回だけrotateForceに+=をしたい </summary>
    public bool swipe = false;
    /// <summary> プレイヤーの回転速度</summary>
    public float RotateSpeed { get; private set; }

    /// <summary>進行方向とみてる方向の角度 </summary>
    public float forwardToLookAngle = 0;

    /// <summary>調整する高さの位置。この値から一定値を超えてズレたらこの高さにプレイヤーの高さを調整する </summary>
    Vector3 AdjustHeightPosition;


    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        AdjustHeightPosition = gameObject.transform.position;
    }
    // Update is called once per frame
    private void Update()
    {
        Speed = m_rb.velocity.magnitude;
    }
    void FixedUpdate()
    {
        //if (!GameState.Instance.InGame)
        //{
        //    return;
        //}


        if (mouthDebug)
        {
            MouthAim();
            MouthForce();
        }
        else
        {
            TouchMoveForce();
            Swipe();
        }

        AirBrake();
        if (m_rb.velocity.magnitude > maxSpeed) //現在の速度が最大速度を超えていたら空気抵抗を上げる
        {
            SpeedLimit();
        }
        RotatePlayer();

        if (Mathf.Abs(transform.position.y - AdjustHeightPosition.y) > 0.01f) //初期位置より一定値を超えて高さがズレていたら位置調整する
        {
            SetHeightAdjust();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        SetProgressAngle();
    }


    /// <summary>加減速を計算する</summary>
    void TouchMoveForce()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                if (!swipe) //前のフレームでスワイプしていなかったとき指を離したら加速する。
                {
                    m_rb.AddForce(this.transform.forward * forwardForce);
                }
            }
        }
    }


    /// <summary>
    /// スピードの最高速度を設定。超過したら空気抵抗を強める
    /// </summary>
    void SpeedLimit()
    {
        m_rb.velocity = m_rb.velocity * maxSpeedExcessBrake;
    }

    /// <summary>
    /// スワイプした距離を測る関数
    /// </summary>
    void Swipe()
    {
        // 画面タッチが行われたら
        if (touch.phase == TouchPhase.Began)
        {
            //touchBeginPos = touch.position;
            swipeDistance_x = 0;
            swipeDistance_y = 0;
        }

        if (touch.phase == TouchPhase.Moved)
        {
            swipe = true;

            swipeDistance_x = touch.deltaPosition.x / Screen.width;
            swipeDistance_y = touch.deltaPosition.y / Screen.height;
        }
        if (touch.phase == TouchPhase.Ended)
        {
            swipe = false;
        }
    }

    /// <summary>
    /// ユーザーのスワイプした距離をプレイヤーの回転を反映する関数
    /// </summary>
    void RotatePlayer()
    {
        //プレイヤーのスワイプ以外での回転を無くす
        if (m_rb.angularVelocity.magnitude > 0)
        {
            m_rb.angularVelocity = m_rb.angularVelocity * 0;
        }

        if (swipe || mouthDebug)
        {
            RotateSpeed = horizontalSpeed * swipeDistance_x;
        }
        else
        {
            RotateSpeed *= rotateBrekeCoefficient;
            if (Mathf.Abs(RotateSpeed) <= 0.01f)
            {
                RotateSpeed = 0;
            }
        }
        transform.Rotate(0, RotateSpeed, 0);
    }

    /// <summary>
    /// 空気抵抗を処理する。
    /// 横を向かせるようにスワイプするとブレーキが強まる。
    /// 一定値未満のスピードだと止まる
    /// </summary>
    void AirBrake()
    {
        m_rb.velocity = m_rb.velocity * airBrekeCoefficient;
        //if (Mathf.Abs(forwardToLookAngle) > addAirBrakeStart) //横向いた時の追加ブレーキを判定する
        //{
        //    m_rb.velocity = m_rb.velocity * addAirBrake;
        //}

        if (swipeDistance_y < -beginSwipeBrake)
        {
            m_rb.velocity = m_rb.velocity * addAirBrake * GetSwipeYaxisRate();
        }

        if (m_rb.velocity.magnitude < 0.01f)
        {
            m_rb.velocity = m_rb.velocity * 0;
        }
    }

    float GetSwipeYaxisRate()
    {
        float swipeYaxisRate = 0;
        swipeYaxisRate = 1 - Mathf.InverseLerp(swipeDistance_y, maxSwipeDistance_y, swipeYaxisRate);
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
        m_rb.AddForce(new Vector3(0, AdjustHeightPosition.y - transform.position.y, 0));
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
    void MouthForce()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_rb.AddForce(this.transform.forward * forwardForce);
        }
        else if (!Input.GetMouseButton(0))
        {
            //m_rb.velocity = m_rb.velocity * coefficient;
        }
    }

    /// <summary>
    /// デバッグ用。プレイヤーがマウスの方を向く
    /// </summary>
    void MouthAim()
    {
        mouthPosi = Input.mousePosition;
        swipeDistance_x = mouthPosi.x / Screen.width;
    }
}
