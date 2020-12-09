using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerGyroRotater : MonoBehaviour
{
    /// <summary>ジャイロ回転を取得する変数</summary>
    public Quaternion m_gyro;
    public Quaternion startGyroRotation;
    [SerializeField, Range(0, 1f)] public float m_gyroCoefficient = 1;

    public float m_gyroRotateSpeed;
    [SerializeField] float m_rotateMinGyroAngle = 0.1f;
    [SerializeField] float m_rotateMaxGyroAngle = 0.4f;
    


    void Start()
    {
        Input.gyro.enabled = true;
        m_gyro = Input.gyro.attitude;
        startGyroRotation = m_gyro;
    }

    void FixedUpdate()
    {
        GetGyroValue();

        if (Mathf.Abs(m_gyro.y) > m_rotateMinGyroAngle)
        {
            RotatePlayerByGyro();
        }

        //transform.localRotation = m_gyro;
    }

    void GetGyroValue()
    {
        m_gyro = Input.gyro.attitude;
        m_gyro.x = 0;
        m_gyro.y *= -1;
        m_gyro.y -= startGyroRotation.y;
        m_gyro.z = 0;

    }

    void RotatePlayerByGyro()
    {
        float rotatePer;
        if (m_gyro.y > 0)
        {
            rotatePer = Mathf.InverseLerp(m_rotateMinGyroAngle, m_rotateMaxGyroAngle, Mathf.Abs(m_gyro.y));
        }
        else
        {
            rotatePer = Mathf.InverseLerp(m_rotateMinGyroAngle, m_rotateMaxGyroAngle, Mathf.Abs(m_gyro.y)) * -1;
        }


        transform.Rotate(0, rotatePer, 0);
    }
}
