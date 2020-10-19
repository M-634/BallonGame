using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// プレイヤーの衝突イベントをまとめるベースクラス
/// </summary>
public class CollisionEvent :MonoBehaviour
{
    [SerializeField] protected UnityEvent  m_OnCollissionEvent;

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
    /// 派生クラスで追加したいs衝突時のイベント
    /// </summary>
    public virtual void AddEvent() { }
}
