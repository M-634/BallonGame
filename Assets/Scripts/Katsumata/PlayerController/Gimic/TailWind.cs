using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailWind : MonoBehaviour
{
    Rigidbody m_rb;
    [Header("追加の加速度")]
    /// <summary>加速する係数 </summary>
    [SerializeField] float m_addCoefficient = 50;

    [Header("加速後最大速度")]
    /// <summary>加速する係数 </summary>
    [SerializeField] float m_accelMaxSpeed = 50;

    KatsumataPlayerCameraAddforce cameraMove;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_rb = other.GetComponent<Rigidbody>();
            cameraMove = other.gameObject.GetComponent<KatsumataPlayerCameraAddforce>();
            if (!cameraMove.cameraFixed)
            {
                cameraMove.ChangeAddSpeedCamera();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && m_rb.velocity.magnitude < m_accelMaxSpeed)
        {
            m_rb.AddForce(other.transform.forward * m_addCoefficient); //ローカル座標でのforward
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !cameraMove.cameraFixed)
        {
            float cameraChangeTime = other.GetComponent<KatsumataPlayerCameraAddforce>().m_cameraChangeTime;
            cameraMove.ChangeBaseCamera(cameraChangeTime);
        }
    }
}
