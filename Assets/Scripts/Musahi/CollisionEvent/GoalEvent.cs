using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalEvent :Sender
{
    public override void CollisionEvent()
    {
        ExecuteGameClearEvent();
        Debug.Log("Goal!!");
    }
}
