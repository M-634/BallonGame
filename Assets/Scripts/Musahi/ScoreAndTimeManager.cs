using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class HighScoreData
{
    public int HighScore;
}

/// <summary>
/// ゲーム中のタイム管理とスコア管理
/// 残り時間に応じてスコアをプラス
/// 時間制限がある
/// ステージの名前をKeyとして、それぞれのハイスコアを保存する
/// </summary>
public class ScoreAndTimeManager : MonoBehaviour
{
    [SerializeField] Text m_timeLimitText;
    [SerializeField] float m_timeLimit = 300f;

    bool m_inGame;
    public bool InGame { get => InGame = m_inGame; set => m_inGame = value; }

    HighScoreData m_highScoreData;
    string m_key;

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
        m_highScoreData = new HighScoreData();
        m_key = SceneManager.GetActiveScene().name;
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

    public void OnGoal()
    {
        InGame = false;
        int m_resultScore = Mathf.FloorToInt(m_timeLimit) * 100;//ここは後で修正するだろう
        //ハイスコアとリザルトスコアを比較する
        if (m_resultScore > LoadHighScore())
        {
            SaveHighScore(m_resultScore);
        }

        Debug.Log("Score: " + m_resultScore);
        Debug.Log("HighScore: " + LoadHighScore());
    }

    /// <summary>
    /// タイムオーバーか、バルーンが障害物に当たったら呼ばれる
    /// </summary>
    public void GameOver()
    {
        InGame = false;
        Debug.Log("GameOver");
    }


    public void SaveHighScore(int score)
    {
        m_highScoreData.HighScore = score;//error
        string json = JsonUtility.ToJson(m_highScoreData, true);
        Debug.Log("シリアライズされた JSONデータ" + json);
        PlayerPrefs.SetString(m_key, json);
    }

    public int LoadHighScore()
    {
        string json = PlayerPrefs.GetString(m_key);
        m_highScoreData = JsonUtility.FromJson<HighScoreData>(json);
        if (m_highScoreData == null)
        {
            //m_highScoreDate変数がインスタンスされない場合があるからここで保険としてインスタンス化している
            m_highScoreData = new HighScoreData();
            return 0;
        }
        return m_highScoreData.HighScore;
    }

    public void ResetHighScore()
    {
        SaveHighScore(0);
    }
}
