using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public enum ItemType
{
    Coin, Diamond, Moon, Star
}
/// <summary>
/// コイン,ダイヤモンド、月、獲得時のイベント
/// </summary>
public class GetItemEvent : MonoBehaviour, IEventCollision
{
    [SerializeField] ItemType m_itemType;
    [SerializeField] int m_getScore;
    [SerializeField] string m_getItemClipName;

    private void Start()
    {
        //アクティブがfalseに回転し続けるからどうにかしたいがDoTweenがうまく動いてくれないので後回し
       // if (m_itemType == ItemType.Coin)
       // {
            transform.DOLocalRotate(Vector3.up * 360, Random.Range(1f, 2f), RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Restart);
       // }
    }

    public void CollisionEvent(EventSystemInGameScene eventSystem)
    {
        SoundManager.Instance.PlayGameSe(m_getItemClipName);
        eventSystem.ExecuteGetItemEvent(m_getScore,m_itemType);
        gameObject.SetActive(false);
    }
}
