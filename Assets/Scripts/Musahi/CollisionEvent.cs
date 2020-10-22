using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// プレイヤーと衝突するオブジェクのイベントをまとめたベースクラス
/// </summary>
[RequireComponent(typeof(Collider))]
public abstract class CollisionEvent<T> :MonoBehaviour where T:MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AddEvent();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AddEvent();
        }
    }

    /// <summary>
    /// 衝突時に呼ばれたいイベントを追加する抽象関数
    /// </summary>
    public abstract void AddEvent();
}

