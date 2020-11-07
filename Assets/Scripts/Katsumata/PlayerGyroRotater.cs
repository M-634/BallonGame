using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGyroRotater : MonoBehaviour
{
    // 自信のTransform, 毎フレーム参照すると無駄なので保持する
    //Transform m_transform;
    public Quaternion m_gyro;

    // 調整値
    //readonly Quaternion rotation = Quaternion.Euler(90, 0, 0);

    //void Start()
    //{
    //    //サポートするかの確認
    //    //if (!SystemInfo.supportsGyroscope)
    //    //{
    //    //    m_gyro = new Quaternion(-1, -1, -1, -1);
    //    //    Destroy(this);
    //    //    return;
    //    //}

    //    //m_transform = transform;
    //    Input.gyro.enabled = true;
    //}

    //void Update()
    //{
    //    // ジャイロの値を獲得する
    //    m_gyro = Input.gyro.attitude;

    //    // 自信の回転をジャイロを元に調整して設定する
    //    transform.localRotation = rotation * new Quaternion(0, -m_gyro.y, 0, m_gyro.w);
    //}

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
