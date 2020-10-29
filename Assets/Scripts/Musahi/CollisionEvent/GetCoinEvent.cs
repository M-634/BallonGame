using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// コイン獲得時のイベント
/// </summary>
public class GetCoinEvent :Sender
{
    public override void CollisionEvent()
    {
        ExecuteGetCoinEvent();
        gameObject.SetActive(false);
        Debug.Log("コイン獲得");
    }
}
