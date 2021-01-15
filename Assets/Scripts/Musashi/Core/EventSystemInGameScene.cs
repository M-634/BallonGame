﻿using UnityEngine;
using System;
using System.Collections;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// ゲームシーンで起こるイベントをまとめたクラス
/// </summary>
public class EventSystemInGameScene : MonoBehaviour
{
    public event Action<int,ItemType> GetItemEvent;
    public event Action GameStartEvent;
    public event Action GameOverEvent;
    public event Action GameClearEvent;

    [SerializeField] UISetActiveControl m_ui; 

  
    public void ExecuteGameStartEvent()
    {
        GameStartEvent?.Invoke();
    }

    public void ExecuteGetItemEvent(int score,ItemType itemType)
    {
        GetItemEvent?.Invoke(score,itemType);
    }

    public void ExecuteGameOverEvent()
    {
        GameOverEvent?.Invoke();
        StageParent.Instance.GameClearState = GameClearState.GameOver;
    }

    public void ExecuteGameClearEvent()
    {
        m_ui.GameClearImage.FadeInWithDoTween(2f,() => GameClearEvent?.Invoke());
        //GameClearEvent?.Invoke();
        StageParent.Instance.GameClearState = GameClearState.GameClear;
    }
}