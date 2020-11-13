using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// コイン獲得時のイベント
/// </summary>
public class GetCoinEvent : MonoBehaviour, IEventCollision
{
    /// <summary>コイン獲得時のスコア</summary>
    [SerializeField] int m_getScore;
    public void CollisionEvent(EventSystemInGameScene eventSystem)
    {
        eventSystem.ExecuteGetCoinEvent();
        gameObject.SetActive(false);
        Debug.Log("コイン獲得");
    }
}
