using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGyroController : MonoBehaviour
{
    // 自信のTransform, 毎フレーム参照すると無駄なので保持する
    Transform m_transform;

    // 調整値
    readonly Quaternion rotation = Quaternion.Euler(90, 0, 0);

    void Start()
    {
        // サポートするかの確認
        if (!SystemInfo.supportsGyroscope)
        {
            Destroy(this);
            return;
        }

        m_transform = transform;
    }

    void Update()
    {
        // ジャイロの値を獲得する
        Quaternion gyro = Input.gyro.attitude;

        // 自信の回転をジャイロを元に調整して設定する
        m_transform.localRotation = rotation * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
    }
}
