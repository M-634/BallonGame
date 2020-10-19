using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin :  CollisionEvent
{
    public override void AddEvent()
    {
        this.gameObject.SetActive(false);
        Debug.Log("get coin");
    }
}
