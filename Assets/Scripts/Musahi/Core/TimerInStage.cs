using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// ゲーム中のタイマーの制御（カウントダウン）とそれに伴う
/// ゲームシーンの状態（Start,Goal,GameOver）を監視するクラス
/// </summary>
[RequireComponent(typeof(ScoreManager))]
public class TimerInStage : Reciver
{
    [SerializeField] Text m_timeLimitText;
    [SerializeField] float m_timeLimit = 300f;

    [Header("UI")]
    [SerializeField] Canvas m_gameUI;
    [SerializeField] Canvas m_GameOverUI;
    [SerializeField] Canvas m_GameClearUI;

    ScoreManager m_scoreManager;

    /// <summary>ゲーム中かどうか判定する </summary>
    public bool InGame { get; set; }
    private float m_oldSeconds;//1フレーム前の秒数

     protected override void Start()
    {
        base.Start();
        m_scoreManager = GetComponent<ScoreManager>();
        //ステージを出現させる
        StageParent.Instance.AppearanceStageObject();
        //各UIの表示を設定する
        m_gameUI.gameObject.SetActive(true);
        m_GameOverUI.gameObject.SetActive(false);
        m_GameClearUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!InGame) return;

        //タイムリミット
        m_timeLimit -= Time.deltaTime;
        //分と秒を設定
        int minute = (int)m_timeLimit / 60;
        float seconds = m_timeLimit - minute * 60;
        //UIに00:00形式で表示する
        if ((int)seconds != (int)m_oldSeconds)
        {
            m_timeLimitText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }
        m_oldSeconds = seconds;

        //タイマーリミット!
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
    /// ゴール時のイベントから呼ばれる
    /// </summary>
    public void OnGoal()
    {
        InGame = false;
        m_gameUI.gameObject.SetActive(false);
        m_GameClearUI.gameObject.SetActive(true);
        //残り時間をScoreManagerに渡す
        m_scoreManager.Result(Mathf.FloorToInt(m_timeLimit));
        UnSubscribe();
    }

    /// <summary>
    /// タイムオーバーか、バルーンが障害物に当たったら呼ばれる
    /// </summary>
    public void GameOver()
    {
        InGame = false;
        m_gameUI.gameObject.SetActive(false);
        m_GameOverUI.gameObject.SetActive(true);
        Debug.Log("GameOver");

        //セレクト画面に戻る
        UnSubscribe();
    }

    protected override void Subscribe()
    {
        Sender.GameClearEvent += () => OnGoal();
        Sender.GameOverEvent += () => GameOver();
        base.Subscribe();
    }

    protected override void UnSubscribe()
    {
        Sender.GameClearEvent -= () => OnGoal();
        Sender.GameOverEvent -= () => GameOver();
        base.UnSubscribe();
    }
}
