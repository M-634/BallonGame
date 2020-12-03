using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventHandller : EventReceiver<Test_Player_Musashi>
{
    public bool InGame { get; set; }

    //TO DO:川嶋が加えたよ playerの動きだけをテストしたい時にチェックをいれてね
    [SerializeField] bool m_doDebugPlayerMove;
    ForcePlayerMove m_playerMove;

    protected override void Awake()
    {
        if (m_doDebugPlayerMove) return;
        m_playerMove = GetComponent<ForcePlayerMove>();
        m_playerMove.m_rb.constraints = RigidbodyConstraints.FreezePosition;
        base.Awake();
    }
    

    /// <summary>
    /// 障害物とか
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IEventCollision>(out var eventCollision))
        {
            eventCollision.CollisionEvent(m_eventSystemInGameScene);
        }
    }

    /// <summary>
    /// コインとか
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IEventCollision>(out var eventCollision))
        {
            eventCollision.CollisionEvent(m_eventSystemInGameScene);
        }
    }

    public void GameStart()
    {
        m_playerMove.m_rb.constraints = RigidbodyConstraints.None;
        InGame = true;
        
    }

    public void EndGaem()
    {
        InGame = false;
        GetComponent<PlayerBaseMove>().StopMove();
    }

    protected override void OnEnable()
    {
        if (m_doDebugPlayerMove)
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
        if (m_doDebugPlayerMove) return;
        m_eventSystemInGameScene.GameStartEvent -= GameStart;
        m_eventSystemInGameScene.GameClearEvent -= EndGaem;
        m_eventSystemInGameScene.GameOverEvent -= EndGaem;
    }
}
