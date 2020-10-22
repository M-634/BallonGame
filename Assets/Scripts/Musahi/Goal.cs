using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Goal : CollisionEvent
{
    public override void AddEvent()
    {
        Debug.Log("Goal");
    }
}
