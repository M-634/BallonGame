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
    /// <summary>横向いた時の追加速度の減衰比率の適用時のrotateSpeedの値 </summary>
    [SerializeField] public float addAirBrakeStart = 1f;
    /// <summary>速度を格納する。現状DebugUIに値を渡してる </summary>
    public float Speed { get; private set; }
    /// <summary>速度制限をする。最大速度 </summary>
    public float maxSpeed = 50;
    /// <summary>最大速度を超過したときにかかるブレーキの係数 </summary>
    [SerializeField, Range(0, 1f)] public float maxSpeedExcessBrake = 0.9f;
    /// <summary>スワイプした時にどの程度指に付いてくるかの係数 </summary>
    [SerializeField] public float horizontalSpeed = 35.0f;

    /// <summary>touchを格納、画面タッチをしてる一本目の指を取得する。現状指一本 </summary>
    Touch touch;
    /// <summary>一本目の指のタッチしてる座標を取得する </summary>
    Vector2 touchPos = new Vector2();
    /// <summary>一本目の指のタッチしてる座標を取得し、スワイプするときの最初に触れた場所 </summary>
    Vector2 touchBeginPos;
    /// <summary>x軸のスワイプの動きを格納する</summary>
    public float swipeDistance_x = 0;

    [SerializeField] GameObject debugUIobj;

    /// <summary>unity上でマウスを使ってデバッグを行う時にフラグをオンにする </summary>
    [SerializeField] bool mouthDebug;
    Vector3 mouthPosi;

    /// <summary>スワイプをしたかどうかのフラグ。回転力を加えるとき一回だけrotateForceに+=をしたい </summary>
    bool swipe = false;
    /// <summary> プレイヤーの回転速度</summary>
    public float RotateSpeed { get; private set; }

    /// <summary>進行方向とみてる方向の角度 </summary>
    public float forwardToLookAngle = 0;




    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    private void Update()
    {
        Speed = m_rb.velocity.magnitude;
    }
    void FixedUpdate()
    {
        //if (!TimeScheduler.Instance.InGame)
        //{
        //    return;
        //}
        if (touch.phase == TouchPhase.Moved)
        {
            touchBeginPos = touch.position;
        }

        CalculateForwardToLookAngle();

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
        if (m_rb.velocity.magnitude > maxSpeed)
        {
            SpeedLimit();
        }
        RotatePlayer();
    }

    /// <summary>加減速を計算する</summary>
    void TouchMoveForce()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                m_rb.AddForce(this.transform.forward * forwardForce);
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
            touchBeginPos = touch.position;
            swipeDistance_x = 0;
        }

        if (touch.phase == TouchPhase.Moved)
        {
            swipe = true;
            
            swipeDistance_x = touch.position.x - touchBeginPos.x; //現状DebugUI用に変数作って取得してる
            touchPos = new Vector2(
            (swipeDistance_x) / Screen.width, 0);
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
            RotateSpeed = horizontalSpeed * touchPos.x;
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
        swipe = false;
    }

    /// <summary>
    /// 空気抵抗を処理する。
    /// 横を向かせるようにスワイプするとブレーキが強まる。
    /// 一定値未満のスピードだと止まる
    /// </summary>
    void AirBrake()
    {
        m_rb.velocity = m_rb.velocity * airBrekeCoefficient;
        if (Mathf.Abs(forwardToLookAngle) > addAirBrakeStart)
        {
            m_rb.velocity = m_rb.velocity * addAirBrake;
        }

        if (m_rb.velocity.magnitude < 0.01f)
        {
            m_rb.velocity = m_rb.velocity * 0;
        }
    }

    /// <summary>
    /// 進行方向とみている方向の角度を計算する
    /// </summary>
    void CalculateForwardToLookAngle()
    {
        forwardToLookAngle = Vector3.Angle(m_rb.velocity, transform.forward);
    }

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
        touchPos.x = mouthPosi.x / Screen.width;
    }
}
