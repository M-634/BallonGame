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
public class TimerInStage : EventReceiver<TimerInStage>
{
    [SerializeField] Text m_timeLimitText;
    [SerializeField] float m_timeLimit = 300f;

    ScoreManager m_scoreManager;

    /// <summary>ゲーム中かどうか判定する </summary>
    public bool InGame { get; set; }
    private float m_oldSeconds;//1フレーム前の秒数

    private void Start()
    {
        m_scoreManager = GetComponent<ScoreManager>();
        //ステージを出現させる
        if (StageParent.Instance)
        {
            StageParent.Instance.AppearanceStageObject(StageParent.Instance.GetAppearanceStage.transform);
        }
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
        //残り時間をScoreManagerに渡す
        m_scoreManager.Result(Mathf.FloorToInt(m_timeLimit));
    }

    /// <summary>
    /// タイムオーバーか、バルーンが障害物に当たったら呼ばれる
    /// </summary>
    public void GameOver()
    {
        InGame = false;
        if (StageParent.Instance)
        {
            //ステージを非表示にする
            StageParent.Instance.GetAppearanceStage.SetActive(false);
            //ステージを初期化する
            StageParent.Instance.Initialization();
        }

        if (SceneLoader.Instance)
        {
            //タップしたらセレクト画面に戻る(タップしてください。みたいなテキストを出す)
            SceneLoader.Instance.LoadSelectSceneWithTap();
        }
    }

    protected override void OnEnable()
    {
        m_eventSystemInGameScene.GameStartEvent += StartGame;
        m_eventSystemInGameScene.GameClearEvent += OnGoal;
        m_eventSystemInGameScene.GameOverEvent += GameOver;
    }

    protected override void OnDisable()
    {
        m_eventSystemInGameScene.GameStartEvent -= StartGame;
        m_eventSystemInGameScene.GameClearEvent -= OnGoal;
        m_eventSystemInGameScene.GameOverEvent -= GameOver;
    }
}
