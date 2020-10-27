using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// コイン獲得時のイベント
/// </summary>
public class GetCoinEvent :MonoBehaviour, IEventCollision
{
     ScoreManager m_scoreManager;

    private void Awake()
    {
        gameObject.SetActive(true);
        m_scoreManager = FindObjectOfType<ScoreManager>();
        if (m_scoreManager == null)
        {
            Debug.LogError("ScoreManagerコンポーネントをアタッチされたGameObjectが存在しません");
        }
    }

    /// <summary>
    /// コイン獲得
    /// </summary>
    public void CollisionEvent()
    {
        Debug.Log("コイン獲得した！");
        m_scoreManager.GetCoin();
        gameObject.SetActive(false);
    }
}
