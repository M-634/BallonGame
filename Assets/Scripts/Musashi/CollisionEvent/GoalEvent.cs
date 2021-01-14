using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalEvent :MonoBehaviour,IEventCollision
{
    [SerializeField] string m_gameClearAudioClip;
    public void CollisionEvent(EventSystemInGameScene eventSystem)
    {
        SoundManager.Instance.StopBGMWithFadeOut(0.2f);
        SoundManager.Instance.PlayGameSe(m_gameClearAudioClip);
        eventSystem.ExecuteGameClearEvent();
    }
}
