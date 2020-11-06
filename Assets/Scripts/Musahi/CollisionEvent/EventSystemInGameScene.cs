using UnityEngine;
using System;
using System.Collections;
using System.Net.NetworkInformation;
using System.Collections.Generic;

/// <summary>
/// ゲームシーンで起こるイベントをまとめたクラス
/// ゲームシーン内にあるオブジェクトにアタッチする
/// インスタンスをプレイヤーに持たせる
/// </summary>
public class EventSystemInGameScene : MonoBehaviour
{
    public event Action GetCoinEvent;
    public event Action GameStartEvent;
    public event Action GameOverEvent;
    public event Action GameClearEvent;

    //ToDo:このクラスはサービスである。他のクラスに依存しないように設計し直す 
    [Header("イベントのリスナー")]
    [SerializeField] TimerInStage m_timer;
    [SerializeField] ScoreManager m_scoreManager;
    [SerializeField] UISetActiveControl m_UISetActiveControl;

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        UnSubscribe(); 
    }

    //ToDo: ここを各クラスで行うように修正する
    private void Subscribe()
    {
        GetCoinEvent += m_scoreManager.GetCoin;

        GameStartEvent += m_UISetActiveControl.InisitializeUISetAcitve;

        GameClearEvent += m_UISetActiveControl.UISetActiveWithGameClear;
        GameClearEvent += m_timer.OnGoal;

        GameOverEvent += m_timer.GameOver;
        GameOverEvent += m_UISetActiveControl.UISetActiveWithGameOver;
        Debug.Log("subscribe event....");
    }

    //ToDo: ここを各クラスで行うように修正する
    private void UnSubscribe()
    {
        GetCoinEvent -= m_scoreManager.GetCoin;

        GameStartEvent -= m_UISetActiveControl.InisitializeUISetAcitve;

        GameClearEvent -= m_timer.OnGoal;
        GameClearEvent -= m_UISetActiveControl.UISetActiveWithGameClear;

        GameOverEvent -= m_timer.GameOver;
        GameOverEvent -= m_UISetActiveControl.UISetActiveWithGameOver;
        Debug.Log("unSubscribe event....");
    }

    public void ExecuteGameStartEvent()
    {
        GameStartEvent?.Invoke();
    }

    public void ExecuteGetCoinEvent()
    {
        GetCoinEvent?.Invoke();
    }

    public void ExecuteGameOverEvent()
    {
        GameOverEvent?.Invoke();
    }

    public void ExecuteGameClearEvent()
    {
        GameClearEvent?.Invoke();
    }
}