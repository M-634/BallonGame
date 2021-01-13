using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

/// <summary>
/// コイン獲得時のイベント
/// </summary>
public class GetCoinEvent : MonoBehaviour, IEventCollision
{
    /// <summary>コイン獲得時のスコアを設定してね</summary>
    [SerializeField] int m_getScore;
    [SerializeField] string m_getCoinClipName;

    private void Start()
    {
        //アクティブがfalseに回転し続けるからどうにかしたいがDoTweenがうまく動いてくれないので後回し
        transform.DOLocalRotate(Vector3.up * 360, Random.Range(1f, 2f), RotateMode.FastBeyond360)
             .SetEase(Ease.Linear)
             .SetLoops(-1, LoopType.Restart);
    }

    public void CollisionEvent(EventSystemInGameScene eventSystem)
    {
        SoundManager.Instance.PlayGameSe(m_getCoinClipName);
        eventSystem.ExecuteGetCoinEvent(m_getScore);
        gameObject.SetActive(false);
    }
}
