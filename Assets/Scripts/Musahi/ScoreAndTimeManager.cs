using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// ゲーム中のタイム管理とプレイヤーがゴール、又はゲームオーバー時の
/// スコアの状態を監視するクラス
/// </summary>
public class ScoreAndTimeManager : MonoBehaviour
{
    [SerializeField] Text m_timeLimitText;
    [SerializeField] float m_timeLimit = 300f;

    bool m_inGame;
    public bool InGame { get => InGame = m_inGame; set => m_inGame = value; }

    //singlton
    private static ScoreAndTimeManager m_instance;
    public static ScoreAndTimeManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<ScoreAndTimeManager>();
                if (m_instance == null)
                {
                    Debug.LogError("ScoreAndTimeManagerをアタッチしているGameObjectはありません");
                }
            }
            return m_instance;
        }
    }

    private void Awake()
    {
        if (m_instance != null && this != Instance)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!InGame) return;

        //タイムリミット
        m_timeLimit -= Time.deltaTime;
        m_timeLimitText.text = m_timeLimit.ToString("F2") + "秒";

        if (m_timeLimit <= 0)
        {
            m_timeLimit = 0;
            GameOver();
        }
    }

    public void StartGame()
    {
        InGame = true;
    }

    /// <summary>
    /// 残り時間に応じてスコアをプラス
    /// 時間制限がある
    /// </summary>
    public void OnGoal()
    {
        InGame = false;
        int m_resultScore = Mathf.FloorToInt(m_timeLimit) * 100;//ここは後で修正するだろう
        Debug.Log("Score: " + m_resultScore);
        Debug.Log("Goal!!");
    }

    /// <summary>
    /// タイムオーバーか、バルーンが障害物に当たったら呼ばれる
    /// </summary>
    public void GameOver()
    {
        InGame = false;
        Debug.Log("GameOver");
    }
}
