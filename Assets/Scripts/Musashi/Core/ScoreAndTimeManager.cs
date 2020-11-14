//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//using System.IO;
//using UnityEditor;

//[System.Serializable]
//public class HighScoreData
//{
//    public int HighScore;
//}
////*****
////シングルトンのクラスにセーブ機能があるのはまずいので
////機能のに応じてクラスを分ける
////time score save
////*****

///// <summary>
///// ゲーム中のタイム管理とスコア管理
///// 残り時間に応じてスコアをプラス
///// 時間制限がある
///// ステージの名前をpathとして、それぞれのハイスコアを保存する
///// </summary>
//public class ScoreAndTimeManager : MonoBehaviour
//{
//    [SerializeField] Text m_timeLimitText;
//    [SerializeField] float m_timeLimit = 300f;

//    bool m_inGame;
//    public bool InGame { get => InGame = m_inGame; set => m_inGame = value; }

//    HighScoreData m_highScoreData;
//    public int m_currentGetScore;
//    int m_HighScore;
//    string m_path;

//    //singlton
//    private static ScoreAndTimeManager m_instance;
//    public static ScoreAndTimeManager Instance
//    {
//        get
//        {
//            if (m_instance == null)
//            {
//                m_instance = FindObjectOfType<ScoreAndTimeManager>();
//                if (m_instance == null)
//                {
//                    Debug.LogError("ScoreAndTimeManagerをアタッチしているGameObjectはありません");
//                }
//            }
//            return m_instance;
//        }
//    }

//    private void Awake()
//    {
//        if (m_instance != null && this != Instance)
//        {
//            Destroy(this);
//        }
//    }

//    private void Start()
//    {
//        m_path = Application.dataPath + $"/{SceneManager.GetActiveScene().name}_HighScoreData.json";
//        m_highScoreData = new HighScoreData();
//        m_HighScore = LoadHighScore();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (!InGame) return;

//        //タイムリミット
//        m_timeLimit -= Time.deltaTime;
//        m_timeLimitText.text = m_timeLimit.ToString("F2") + "秒";

//        if (m_timeLimit <= 0)
//        {
//            m_timeLimit = 0;
//            GameOver();
//        }
//    }

//    public void StartGame()
//    {
//        InGame = true;
//    }

//    public void OnGoal()
//    {
//        InGame = false;
//        int m_resultScore = Mathf.FloorToInt(m_timeLimit) * 100 + m_currentGetScore;//ここは後で修正するだろう
//        //ハイスコアとリザルトスコアを比較する
//        if (m_resultScore > m_HighScore)
//        {
//            SaveHighScore(m_resultScore);
//            m_HighScore = m_resultScore;
//        }
//        Debug.Log("Score: " + m_resultScore);
//        Debug.Log("HighScore: " + m_HighScore);
//    }

//    /// <summary>
//    /// タイムオーバーか、バルーンが障害物に当たったら呼ばれる
//    /// </summary>
//    public void GameOver()
//    {
//        InGame = false;
//        Debug.Log("GameOver");
//    }

//    public void AddScore(int score)
//    {
//        m_currentGetScore += score;
//    }

//    public void SaveHighScore(int score)
//    {
//        m_highScoreData.HighScore = score;//error
//        string json = JsonUtility.ToJson(m_highScoreData, true);
//        Debug.Log("シリアライズされた JSONデータ" + json);
        
//        //ハイスコアを上書きする
//        StreamWriter writer = new StreamWriter(m_path, false);
//        writer.Write(json);
//        writer.Flush();
//        writer.Close();
//        Debug.Log("Saving HighScore.....");
//    }

//    public int LoadHighScore()
//    {
//        if (!File.Exists(m_path))
//        {
//            //make file
//            SaveHighScore(0);
//            Debug.Log("Initialize file.....");
//            return 0;
//        }

//        StreamReader reader = new StreamReader(m_path);
//        string json = reader.ReadToEnd();
//        reader.Close();
//        m_highScoreData = JsonUtility.FromJson<HighScoreData>(json);
//        Debug.Log("Loading HighScore.....");
//        return m_highScoreData.HighScore;
//    }

//    public void ResetHighScore()
//    {
//        SaveHighScore(0);
//    }
//}
