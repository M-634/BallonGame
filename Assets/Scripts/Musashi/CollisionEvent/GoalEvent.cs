using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalEvent :MonoBehaviour,IEventCollision
{
    public void CollisionEvent(EventSystemInGameScene eventSystem)
    {
        SoundManager.Instance.StopBGMWithFadeOut(0.2f);
        SoundManager.Instance.StopEnviromet();
        SoundManager.Instance.PlayGameSe("GoalTouch");
        eventSystem.ExecuteGameClearEvent();
    }
}
