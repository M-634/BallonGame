using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalEvent : CollisionEvent<GoalEvent>
{
    public override void AddEvent()
    {
        TimeScheduler.Instance.OnGoal();
        Debug.Log("Goal");
    }
}
