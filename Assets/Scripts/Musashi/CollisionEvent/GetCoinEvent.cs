using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// コイン獲得時のイベント
/// </summary>
public class GetCoinEvent : MonoBehaviour, IEventCollision
{
    /// <summary>コイン獲得時のスコアを設定してね</summary>
    [SerializeField] int m_getScore;
    [SerializeField] string m_getCoinClipName;

    public void CollisionEvent(EventSystemInGameScene eventSystem)
    {
        SoundManager.Instance.PlayGameSe(m_getCoinClipName);
        eventSystem.ExecuteGetCoinEvent(m_getScore);
        gameObject.SetActive(false);
    }
}
