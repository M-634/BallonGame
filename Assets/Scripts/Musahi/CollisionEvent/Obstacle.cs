using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全ての障害物のベースクラス.
/// プレイヤーに当たるとゲームオーバーする
/// </summary>
public class Obstacle :MonoBehaviour, IEventCollision
{
    protected TimerInStage m_gameState;

    virtual protected void Awake()
    {
        //m_gameState = FindObjectOfType<TimerInStage>();
        //if (m_gameState == null)
        //{
        //    Debug.LogError("GameStateコンポーネントをアタッチされたGameObjectが存在しません");
        //}
    }

    virtual protected void OnEnable()
    {
        m_gameState = FindObjectOfType<TimerInStage>();
        if (m_gameState == null)
        {
            Debug.LogError("GameStateコンポーネントをアタッチされたGameObjectが存在しません");
        }
    }
    /// <summary>
    /// ゲームオーバー
    /// </summary>
    virtual public void CollisionEvent()
    {
        m_gameState.GameOver();
        Debug.Log("ゲームオーバー");
    }
}
