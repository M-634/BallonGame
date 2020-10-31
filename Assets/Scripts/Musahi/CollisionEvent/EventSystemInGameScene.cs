using UnityEngine;
using System;
using System.Collections;
using System.Net.NetworkInformation;

/// <summary>
/// ゲームシーンで起こるイベントをまとめたクラス
/// ゲームシーン内にあるオブジェクトにアタッチする
/// </summary>
public class EventSystemInGameScene : MonoBehaviour
{
    private event Action GetCoinEvent;
    private event Action GameOverEvent;
    private event Action GameClearEvent;

    [Header("イベントのリスナー")]
    [SerializeField] TimerInStage m_timer;
    [SerializeField] ScoreManager m_scoreManager;

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        UnSubscribe(); 
    }

    private void Subscribe()
    {
        GetCoinEvent += m_scoreManager.GetCoin;
        GameClearEvent += m_timer.OnGoal;
        GameOverEvent += m_timer.GameOver;
        Debug.Log("subscribe event....");
    }

    private void UnSubscribe()
    {
        GetCoinEvent -= m_scoreManager.GetCoin;
        GameClearEvent -= m_timer.OnGoal;
        GameOverEvent -= m_timer.GameOver;
        Debug.Log("unSubscribe event....");
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