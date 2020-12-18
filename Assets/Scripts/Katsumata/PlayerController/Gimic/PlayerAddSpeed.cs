using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(KatsumataPlayerCameraAddforce))]
/// <summary>
/// 物理的加速、マリオカートのキノコダッシュを想定している
/// </summary>
public class PlayerAddSpeed : MonoBehaviour
{
    Rigidbody m_rb;
    [Header("追加の加速度")]
    /// <summary>加速する係数 </summary>
    [SerializeField] float m_addCoefficient = 50;
    /// <summary>次に加速が始まるまでの待機時間 </summary>
    //[SerializeField] float m_accelWaitTime = 2.5f;

    KatsumataPlayerCameraAddforce cameraMove;

    bool m_onAccel = false;
    [SerializeField] float accelMaxSpeed = 20.0f;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        cameraMove = GetComponent<KatsumataPlayerCameraAddforce>();
    }

    private void FixedUpdate()
    {
        //if (!m_onAccelable) //次の加速までのインターバルを計算する
        //{
        //    m_waitTime += Time.deltaTime; //タイムスケールを0にすると計算は止まる
        //    if (m_waitTime > m_accelWaitTime)
        //    {
        //        m_onAccelable = true;
        //        cameraMove.ChangeBaseCamera();
        //    }
        //    else
        //    {
        //        m_onAccelable = false;
        //    }
        //}
        //Debug.Log("waittime : " + m_waitTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "acceleration")
        {
            m_onAccel = true;
            cameraMove.ChangeAddSpeedCamera();
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "acceleration"&& m_rb.velocity.magnitude < accelMaxSpeed)
        {
            m_rb.AddForce(other.transform.forward * m_addCoefficient); //ローカル座標でのforward
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "acceleration")
        {
            m_onAccel = false;
            cameraMove.ChangeBaseCamera();
        }
    }
}
