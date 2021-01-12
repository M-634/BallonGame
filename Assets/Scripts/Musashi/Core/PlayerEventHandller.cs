using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*********
*fileをScripts > Mushashiに移動しました。
*ここはムサシ以外にいじるな！
*********/

/// <summary>
/// プレイヤーがゲームシーン上のイベントを購読するクラス
/// </summary>
public class PlayerEventHandller : EventReceiver<PlayerEventHandller>
{
    public bool InGame { get; set; }

    [Header("playerの動きだけをテストしたい時にチェックをいれてね")]
    [SerializeField] bool m_doDebugPlayerMove;
    Rigidbody m_rb;

    protected override void Awake()
    {
        base.Awake();
        if (m_doDebugPlayerMove) return;
        m_rb = GetComponent<Rigidbody>();
        m_rb.constraints = RigidbodyConstraints.FreezePosition;
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
        m_rb.constraints = RigidbodyConstraints.None;
        InGame = true;

    }

    public void EndGaem()
    {
        InGame = false;
        m_rb.constraints = RigidbodyConstraints.FreezeAll;
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
