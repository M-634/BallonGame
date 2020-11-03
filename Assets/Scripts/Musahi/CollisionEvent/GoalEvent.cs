using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalEvent :MonoBehaviour,IEventCollision
{
    public void CollisionEvent(EventSystemInGameScene eventSystem)
    {
        eventSystem.ExecuteGameClearEvent();
        Debug.Log("Goal!!");
    }
}
