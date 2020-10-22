using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// プレイヤーと衝突するオブジェクのイベントをまとめたベースクラス
/// </summary>
public abstract class CollisionEvent<T> :MonoBehaviour where T:MonoBehaviour
{
    protected UnityEvent  m_OnCollissionEvent;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_OnCollissionEvent.AddListener(() => AddEvent());
            m_OnCollissionEvent?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_OnCollissionEvent.AddListener(() => AddEvent());
            m_OnCollissionEvent?.Invoke();
        }
    }

    /// <summary>
    /// 衝突時に呼ばれたいイベントを追加する抽象関数
    /// </summary>
    public abstract void AddEvent();
}
