using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(KatsumataPlayerCameraMove))]
/// <summary>
/// 物理的加速、マリオカートのキノコダッシュを想定している
/// </summary>
public class PlayerAddSpeed : MonoBehaviour
{
    Rigidbody m_rb;
    /// <summary>加速する係数 </summary>
    [SerializeField] float m_addCoefficient = 1500;
    /// <summary>次に加速が始まるまでの待機時間 </summary>
    [SerializeField] float m_accelWaitTime = 2.5f;

    KatsumataPlayerCameraMove cameraMove;

    bool m_onAccelable = true;
    //bool m_onAccel = false;
    float m_waitTime = 0;
    //[SerializeField] float m_accelTime = 2.5f;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        cameraMove = GetComponent<KatsumataPlayerCameraMove>();
    }

    private void FixedUpdate()
    {
        if (!m_onAccelable) //次の加速までのインターバルを計算する
        {
            m_waitTime += Time.deltaTime; //タイムスケールを0にすると計算は止まる
            if (m_waitTime > m_accelWaitTime)
            {
                m_onAccelable = true;
                cameraMove.ChangeBaseCamera();
            }
            else
            {
                m_onAccelable = false;
            }
        }
        //Debug.Log("waittime : " + m_waitTime);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "acceleration" && m_onAccelable)
        {
            m_rb.AddForce(transform.forward * m_addCoefficient);
            m_onAccelable = false;
            cameraMove.ChangeAddSpeedCamera();
            m_waitTime = 0;
        }
    }
}
