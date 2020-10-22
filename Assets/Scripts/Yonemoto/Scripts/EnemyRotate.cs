using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotate : MonoBehaviour
{
    public float m_speed;
    public float radius;

    private Vector3 pos;
    private float x, z;

    void Start()
    {
        // positionをオブジェクトの初期値に設定する
        pos = transform.position;
    }

    void Update()
    {
        x = radius * Mathf.Sin(Time.time * m_speed);
        z = radius * Mathf.Cos(Time.time * m_speed);
        transform.position = new Vector3(x + pos.x, pos.y, z + pos.z);
    }
}
