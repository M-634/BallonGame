using UnityEngine;
using System;
using System.Collections;
using System.Net.NetworkInformation;
using System.Collections.Generic;

/// <summary>
/// ゲームシーンで起こるイベントをまとめたクラス
/// </summary>
public class EventSystemInGameScene : MonoBehaviour
{
    public event Action<int> GetCoinEvent;
    public event Action GameStartEvent;
    public event Action GameOverEvent;
    public event Action GameClearEvent;
  
    public void ExecuteGameStartEvent()
    {
        GameStartEvent?.Invoke();
    }

    public void ExecuteGetCoinEvent(int score)
    {
        GetCoinEvent?.Invoke(score);
    }

    public void ExecuteGameOverEvent()
    {
        GameOverEvent?.Invoke();
        StageParent.Instance.GameClearState = GameClearState.GameOver;
    }

    public void ExecuteGameClearEvent()
    {
        GameClearEvent?.Invoke();
        StageParent.Instance.GameClearState = GameClearState.GameClear;
    }
}