﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
/// <summary>
/// デフォルト状態のユーザー制御によるプレイヤーの動きを制御する
/// 上下方向にプレイヤーの向きをスワイプで方向転換し、タッチで加速。
/// ギミック等を受けた場合のプレイヤーが特殊な状態にある場合は別クラスに操作方法を書く
/// </summary>
public class TouchForcePlayerMove : MonoBehaviour
{
    /// <summary>pivotのrigidbodyを格納する変数 </summary>
    public Rigidbody m_rb;
    /// <summary>キャラクターのrigidbodyを格納する変数 </summary>
    //public Rigidbody m_playerChararb;
    ///// <summary>ピボットに対してプレイヤーの受ける力の比率 </summary>
    //[SerializeField] float forceRatio = 0.95f;
    [Header("開始速度")]
    [SerializeField] float startSpeed = 5;


    [Header("最大落下速度")]
    [SerializeField] float maxFallSpeed = 5.0f;

    [Header("最大前進速度")]
    [SerializeField] float maxForwardSpeed = 50.0f;

    //Vector3 gravity = new Vector3(0, -9.81f, 0);
    [Header("浮力")]
    /// <summary>浮力 </summary>
    [SerializeField] float buoyancy = 8.0f;

    [Header("推進力")]
    /// <summary>前進する力。RigidBodyのAddForceで制御する </summary>
    public float m_forwardForce = 200;

    /// <summary>touchを格納、画面タッチをしてる一本目の指を取得する。現状指一本 </summary>
    Touch m_touch;
    /// <summary>x軸のスワイプの動きを格納する</summary>
    [HideInInspector] public float m_swipeDistance_x = 0;
    /// <summary>y軸のスワイプの動きを格納する</summary>
    [HideInInspector] public float m_swipeDistance_y = 0;
    /// <summary>スワイプをしたかどうかのフラグ。回転力を加えるとき一回だけrotateForceに+=をしたい </summary>
    [HideInInspector] public bool m_onSwipe = false;

    /// <summary> プレイヤーの回転速度</summary>
    Vector3 m_rotateSpeed;

    Vector3 m_rotateForce;
    //[Header("回転の減衰比率")]
    /// <summary>回転の減衰比率 </summary>
    //[SerializeField, Range(0, 1f)] public float m_rotateBrekeCoefficient = 0.94f;

    /// <summary画面回転モードかどうか。画面回転でないときスワイプでプレイヤーが回転する </summary>
    //bool isScreenRotation = false;
    //float screenRotationWaitTime = 0;
    //float screenRotationLimitTime = 0;

    [Header("スワイプ時のプレイヤーの回転感度")]
    /// <summary>スワイプした時にどの程度指に付いてくるかの係数 </summary>
    [SerializeField] public float m_horizontalSpeed = 100f;
    /// <summary>スワイプした時にどの程度指に付いてくるかの係数 </summary>
    [SerializeField] public float m_varticalSpeed = 100f;
    /// <summary>unity上でマウスを使ってデバッグを行う時にフラグをオンにする </summary>
    [SerializeField] bool m_mouthDebug;
    Vector3 m_mouthPosi;

    PlayerEventHandller m_playerEventHandller;

    /// <summary> カメラの横軸回転速度</summary>
    public float m_RotateHorizontalSpeed;
    /// <summary> カメラの縦軸回転速度</summary>
    public float m_RotateVarticalSpeed;
    /// <summary>横回転の減衰比率 </summary>
    [SerializeField, Range(0, 1f)] public float m_rotateHorizontalBrake = 0.94f;
    /// <summary>縦回転の減衰比率 </summary>
    [SerializeField, Range(0, 1f)] public float m_rotateVarticalBrake = 0.94f;
    [SerializeField] GameObject playerChar;


    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_playerEventHandller = GetComponent<PlayerEventHandller>();

        m_rb.velocity = transform.forward * startSpeed;
        //m_playerChararb.velocity = transform.forward * startSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!m_playerEventHandller.InGame)
        {
            return;
        }

        if (m_rb.velocity.z < maxForwardSpeed) AddTouchMoveForce();
        AdjustFallingForce();

        if(m_rb.velocity.z<maxForwardSpeed) m_rb.AddForce(transform.forward * m_forwardForce);


        //Debug.Log("m_pivotrb.velocity.x :" + m_rb.velocity.x + "m_pivotrb.velocity.y :"
        //    + m_rb.velocity.y + "m_pivotrb.velocity.z :" + m_rb.velocity.z);
        if (m_mouthDebug)
        {
            SetMouthAim();
        }
        else
        {
            GetSwipeDistance();
        }


        //SetRotateSpeed();
        //SetProgressAngle();
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
            m_RotateHorizontalSpeed = m_horizontalSpeed * m_swipeDistance_x;
            m_RotateVarticalSpeed = m_varticalSpeed * m_swipeDistance_y;
            
        }
        else
        {
            m_RotateHorizontalSpeed *= m_rotateHorizontalBrake;
            m_RotateVarticalSpeed *= m_rotateVarticalBrake;
            if (Mathf.Abs(m_RotateHorizontalSpeed) <= 0.01f)
            {
                m_RotateHorizontalSpeed = 0;
            }

            if (Mathf.Abs(m_RotateVarticalSpeed) <= 0.01f)
            {
                m_RotateVarticalSpeed = 0;
            }
        }
        playerChar.transform.Rotate(m_RotateVarticalSpeed, m_RotateHorizontalSpeed, 0,Space.World);
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
                    m_rb.AddForce(Camera.main.transform.forward * m_forwardForce);
                    //m_playerChararb.AddForce(Camera.main.transform.forward * m_forwardForce * forceRatio);
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_rb.AddForce(Camera.main.transform.forward * m_forwardForce);
                //m_playerChararb.AddForce(Camera.main.transform.forward * m_forwardForce * forceRatio);
            }
        }

    }

    /// <summary>
    /// デバッグ用。マウスの位置を取得する
    /// </summary>
    void SetMouthAim()
    {
        m_mouthPosi = Input.mousePosition;
        m_swipeDistance_x = m_mouthPosi.x / Screen.width;
        m_swipeDistance_y = m_mouthPosi.y / Screen.height;
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
            //m_playerChararb.useGravity = false;
        }
        else
        {
            m_rb.useGravity = true;
            m_rb.AddForce(Vector3.up * buoyancy);
            //m_playerChararb.useGravity = true;
            //m_playerChararb.AddForce(buoyancy);
        }
    }

    /// <summary>
    /// 進行方向の角度をx,z回転を0にするように矯正する。
    /// </summary>
    void SetProgressAngle()
    {
        Quaternion nowAngle = transform.rotation;
        nowAngle.x = 0;
        nowAngle.z = 0;
        nowAngle.y = 0;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, nowAngle, Time.deltaTime);
    }
    ///// <summary>
    ///// 前進速度の最高速度を制限する
    ///// </summary>
    //void AdjustForwardForce()
    //{
    //    if (m_rb.velocity.z < maxForwardSpeed) m_rb.AddForce(transform.forward * tailwind);
    //}
}
