using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventHandller : EventReceiver<Test_Player_Musashi>
{
    public bool InGame { get; set; }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IEventCollision>(out var eventCollision))
        {
            eventCollision.CollisionEvent(m_eventSystemInGameScene);
        }
    }

    public void GameStart()
    {
        InGame = true;
    }

    public void EndGaem()
    {
        InGame = false;
    }

    protected override void OnEnable()
    {
        if (m_doDebug)
        {
            InGame = true;
            return;
        }
        m_eventSystemInGameScene.GameStartEvent += GameStart;
        m_eventSystemInGameScene.GameClearEvent += EndGaem;
        m_eventSystemInGameScene.GameOverEvent += EndGaem;
    }

    protected override void OnDisable()
    {
        if (m_doDebug) return;
        m_eventSystemInGameScene.GameStartEvent -= GameStart;
        m_eventSystemInGameScene.GameClearEvent -= EndGaem;
        m_eventSystemInGameScene.GameOverEvent -= EndGaem;
    }
}
