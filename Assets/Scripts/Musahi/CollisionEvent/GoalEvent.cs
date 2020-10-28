using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalEvent :MonoBehaviour, IEventCollision
{
    TimerInStage m_gameState;

    private void Awake()
    {
        //m_gameState = FindObjectOfType<TimerInStage>();
        //if (m_gameState == null)
        //{
        //    Debug.LogError("GameStateコンポーネントをアタッチされたGameObjectが存在しません");
        //}
    }

    private void OnEnable()
    {
        m_gameState = FindObjectOfType<TimerInStage>();
        if (m_gameState == null)
        {
            Debug.LogError("GameStateコンポーネントをアタッチされたGameObjectが存在しません");
        }
    }


    /// <summary>
    /// ゲームクリア
    /// </summary>
    public void CollisionEvent()
    {
        Debug.Log("ゴール！！");
        m_gameState.OnGoal();
    }
}
