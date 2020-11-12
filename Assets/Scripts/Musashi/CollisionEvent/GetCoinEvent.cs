﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// コイン獲得時のイベント
/// </summary>
public class GetCoinEvent : MonoBehaviour, IEventCollision
{
    public void CollisionEvent(EventSystemInGameScene eventSystem)
    {
        eventSystem.ExecuteGetCoinEvent();
        gameObject.SetActive(false);
        Debug.Log("コイン獲得");
    }
}