using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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

    bool m_onAccelable = true;
    bool m_onAccel = false;
    float m_waitTime = 0;
    [SerializeField] float m_accelTime = 2.5f;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!m_onAccelable)
        {
            m_waitTime += Time.deltaTime; //タイムスケールを0にすると計算は止まる
            m_onAccelable = m_waitTime > m_accelWaitTime ? true : false;
        }
        else
        {
            m_waitTime = 0;
        }
        Debug.Log("waittime : " + m_waitTime);

        if (m_onAccel)
        {

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "acceleration" && m_onAccelable)
        {
            m_rb.AddForce(transform.forward * m_addCoefficient);
            m_onAccelable = false;
            m_onAccel = true;
        }
    }
}
