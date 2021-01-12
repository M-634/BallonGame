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
public class GameSceneManager : EventReceiver<GameSceneManager>
{
    [SerializeField] UISetActiveControl m_UISetActiveControl;

    [Header("Audio")]
    [SerializeField] string m_GameSceneBGMName;
    [SerializeField] string m_GameClearSEName;
    [SerializeField] string m_GameOverSEName;

    ScoreManager m_scoreManager;
    float m_timeLimit;

    /// <summary>ゲーム中かどうか判定する </summary>
    public bool InGame { get; set; }

    private void Start()
    {
        m_scoreManager = GetComponent<ScoreManager>();
        if (StageParent.Instance)
        {
            //ステージを出現させる
            StageParent.Instance.AppearanceStageObject(StageParent.Instance.GetAppearanceStagePrefab.transform);
            //制限時間をセットする
            m_timeLimit = StageParent.Instance.GetAppearanceStageData.SetTimeLimit;
        }

        if (SoundManager.Instance)
        {
            SoundManager.Instance.PlayBGMWithFadeIn(m_GameSceneBGMName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!InGame) return;

        //タイムリミット
        m_timeLimit -= Time.deltaTime;
        //分と秒とミリ秒を設定
        int minutes = (int)m_timeLimit / 60;
        float seconds = m_timeLimit - minutes * 60;
        float mseconds = m_timeLimit * 1000 % 1000;
        m_UISetActiveControl.TimerText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, mseconds);
        //m_UISetActiveControl.TimerText.TimerInfo(m_timeLimit);

        //タイマーリミット!
        if (m_timeLimit <= 0)
        {
            m_timeLimit = 0;
            m_eventSystemInGameScene.ExecuteGameOverEvent();
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
        if (SoundManager.Instance)
        {
            SoundManager.Instance.StopBGMWithFadeOut(m_GameSceneBGMName, 0.1f);
            SoundManager.Instance.PlaySe(m_GameClearSEName);
        }
        //残り時間をScoreManagerに渡す
        m_scoreManager.DisplayResult(m_timeLimit);
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
            StageParent.Instance.GetAppearanceStagePrefab.SetActive(false);
            //ステージを初期化する
            StageParent.Instance.Initialization();
        }

        if (SceneLoader.Instance)
            //タップしたらセレクト画面に戻る(タップしてください。みたいなテキストを出す)
            SceneLoader.Instance.LoadSelectSceneWithTap();

        if (SoundManager.Instance)
        {
            SoundManager.Instance.StopBGMWithFadeOut(m_GameSceneBGMName, 0.1f);
            SoundManager.Instance.PlaySe(m_GameOverSEName);
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
