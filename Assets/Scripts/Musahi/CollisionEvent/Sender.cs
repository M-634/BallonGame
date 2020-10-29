using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UIElements;
/// <summary>
/// イベントを発行するベースクラス
/// memo:イベントのリスナーはScoreManagerとTimerInStageクラス
///</summary>
public abstract class Sender: MonoBehaviour,IEventCollision
{
    public static event Action GetCoinEvent = null;
    public static event Action GameOverEvent = null;
    public static event Action GameClearEvent = null;

    public abstract void CollisionEvent();

    protected virtual void ExecuteGetCoinEvent()
    {
        GetCoinEvent?.Invoke();
    }

    protected virtual void ExecuteGameOverEvent()
    {
        GameOverEvent?.Invoke();
    }

    protected virtual void ExecuteGameClearEvent()
    {
        GameClearEvent?.Invoke();
    }
}
