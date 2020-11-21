using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
/// <summary>
/// デフォルト状態のユーザー制御によるプレイヤーの動きを制御する
/// 11/21中間発表以降の新しいプレイヤーの基本動作クラス
/// 風の影響を受けて出発し、プレイヤーの操作は向きの転換のみ。
/// 風の影響によって加速、減速、上昇、下降する
/// ギミック等を受けた場合のプレイヤーが特殊な状態にある場合は別クラスに操作方法を書く
/// </summary>
public class FreshPlayerBaseMove : MonoBehaviour
{
    /// <summary>rigidbodyを格納する変数 </summary>
    public static Rigidbody m_rb;
    [Header("開始速度")]
    [SerializeField] float startSpeed = 5;
    [Header("自動加速。常にプレイヤーの後ろから吹く風")]
    /// <summary>追い風。常に吹いている </summary>
    //[SerializeField] Vector3 tailwind = new Vector3(0, 0, 5.0f);
    [SerializeField] float tailwind = 5.0f;

    [Header("最大落下速度")]
    [SerializeField] float maxFallSpeed = 5.0f;

    [Header("最大前進速度")]
    [SerializeField] float maxForwardSpeed = 50.0f;

    //Vector3 gravity = new Vector3(0, -9.81f, 0);
    [Header("浮力")]
    /// <summary>浮力 </summary>
    [SerializeField] Vector3 buoyancy = new Vector3(0, 8.0f, 0);

    [Header("重心")]
    /// <summary>重心を設定する。ここを中心にプレイヤーが傾く </summary>
    [SerializeField] GameObject centerOfGravity;

    /// <summary>touchを格納、画面タッチをしてる一本目の指を取得する。現状指一本 </summary>
    Touch m_touch;
    /// <summary>x軸のスワイプの動きを格納する</summary>
    [HideInInspector] public float m_swipeDistance_x = 0;
    /// <summary>y軸のスワイプの動きを格納する</summary>
    [HideInInspector] public float m_swipeDistance_y = 0;
    /// <summary>スワイプをしたかどうかのフラグ。回転力を加えるとき一回だけrotateForceに+=をしたい </summary>
    [HideInInspector] public bool m_onSwipe = false;

    /// <summary> プレイヤーの回転速度</summary>
    public float mp_RotateSpeed { get; private set; }
    [Header("回転の減衰比率")]
    /// <summary>回転の減衰比率 </summary>
    [SerializeField, Range(0, 1f)] public float m_rotateBrekeCoefficient = 0.94f;
    Vector3 latestPos;

    /// <summary画面回転モードかどうか。画面回転でないときスワイプでプレイヤーが回転する </summary>
    bool isScreenRotation = false;
    float screenRotationWaitTime = 0;
    float screenRotationLimitTime = 0;

    [Header("スワイプ時のプレイヤーの回転感度")]
    /// <summary>スワイプした時にどの程度指に付いてくるかの係数 </summary>
    [SerializeField] public float m_horizontalSpeed = 100f;
    /// <summary>unity上でマウスを使ってデバッグを行う時にフラグをオンにする </summary>
    [SerializeField] bool m_mouthDebug;
    Vector3 m_mouthPosi;

    PlayerEventHandller m_playerEventHandller;


    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_playerEventHandller = GetComponent<PlayerEventHandller>();

        m_rb.velocity = transform.forward * startSpeed;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!m_playerEventHandller.InGame) return;

        AdjustFallingForce();
        AdjustForwardForce();

        Debug.Log("m_rb.velocity.x :" + m_rb.velocity.x + "m_rb.velocity.y :" + m_rb.velocity.y + "m_rb.velocity.z :" + m_rb.velocity.z);
        if (m_mouthDebug)
        {
            SetMouthAim();
        }
        else
        {
            GetSwipeDistance();
        }

        IsCheckScreenOperations();

        SetRotateSpeed();
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
            //m_swipeDistance_x = 0;
            //m_swipeDistance_y = 0;
        }
        else if (m_touch.phase == TouchPhase.Moved)
        {
            m_onSwipe = true;

            m_swipeDistance_x = m_touch.deltaPosition.x / Screen.width;
            m_swipeDistance_y = m_touch.deltaPosition.y / Screen.height;
        }
        else if (m_touch.phase == TouchPhase.Ended)
        {
            m_onSwipe = false;
            m_swipeDistance_x = 0;
            m_swipeDistance_y = 0;
        }
    }



    void IsCheckScreenOperations()
    {
        if (m_touch.phase == TouchPhase.Stationary)
        {
            screenRotationWaitTime += Time.deltaTime;
            if (screenRotationWaitTime > screenRotationLimitTime) isScreenRotation = true;
        }
        else
        {
            screenRotationWaitTime = 0;
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

    //void SetPlayerVerticalAngle()
    //{
    //    Vector3 diff = transform.position - latestPos;   //前回からどこに進んだかをベクトルで取得
    //    diff.y = 0;
    //    latestPos = transform.position;  //前回のPositionの更新
    //    //ベクトルの大きさが0.01以上の時に向きを変える処理をする
    //    if (diff.magnitude > 0.01f)
    //    {
    //        transform.rotation = Quaternion.LookRotation(diff); //向きを変更する
    //    }
    //}

    /// <summary>
    /// デバッグ用。マウスの位置を取得する
    /// </summary>
    void SetMouthAim()
    {
        m_mouthPosi = Input.mousePosition;
        m_swipeDistance_x = m_mouthPosi.x / Screen.width;
    }

    /// <summary>
    /// 自然に落ちたり進んだり進む方向
    /// 落下最高速度を超えると落下の加速は止まる
    /// </summary>
    void AdjustFallingForce()
    {
        if (m_rb.velocity.y < -maxFallSpeed)
        {
            m_rb.useGravity = false;
        }
        else
        {
            m_rb.useGravity = true;
            m_rb.AddForce(buoyancy);
        }
    }

    /// <summary>
    /// 前進速度の最高速度を制限する
    /// </summary>
    void AdjustForwardForce()
    {
        if (m_rb.velocity.z < maxForwardSpeed) m_rb.AddForce(transform.forward * tailwind);
    }
}
