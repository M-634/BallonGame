using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerGyroRotater : MonoBehaviour
{
    public Quaternion m_gyro;
    void Start()
    {
        Input.gyro.enabled = true;
    }

    void FixedUpdate()
    {
        m_gyro = Input.gyro.attitude;
        m_gyro.x = 0;
        m_gyro.y *= -1;
        m_gyro.z = 0;

        transform.localRotation = m_gyro;
    }
}
