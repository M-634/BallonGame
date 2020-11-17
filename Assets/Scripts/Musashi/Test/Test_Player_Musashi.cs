using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Player_Musashi : EventReceiver<Test_Player_Musashi>
{
    //TimerInStage m_gameState;
    public float m_speed = 1f;
    private bool m_inGame;

    // Update is called once per frame
    void Update()
    {
        //if (!m_gameState.InGame) return;
        if (!m_inGame) return;

        transform.position += new Vector3(0, 0, m_speed / 90f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IEventCollision>(out var eventCollision))
        {
            eventCollision.CollisionEvent(m_eventSystemInGameScene);
        }
    }

    public void GameStart()
    {
        m_inGame = true;
    }

    public void EndGame()
    {
        m_inGame = false;
    }

    protected override void OnEnable()
    {
        m_eventSystemInGameScene.GameStartEvent += GameStart;
        m_eventSystemInGameScene.GameClearEvent += EndGame;
        m_eventSystemInGameScene.GameOverEvent += EndGame;
    }

    protected override void OnDisable()
    {
        m_eventSystemInGameScene.GameStartEvent -= GameStart;
        m_eventSystemInGameScene.GameClearEvent -= EndGame;
        m_eventSystemInGameScene.GameOverEvent -= EndGame;
    }
}
