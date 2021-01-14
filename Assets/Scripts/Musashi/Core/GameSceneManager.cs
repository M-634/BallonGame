using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using Cinemachine;
using UnityEngine.UI;

/// <summary>
/// ゲーム中のタイマーの制御（カウントダウン）とそれに伴う
/// ゲームシーンの状態（Start,Goal,GameOver）を監視するクラス
/// </summary>
[RequireComponent(typeof(ScoreManager))]
public class GameSceneManager : EventReceiver<GameSceneManager>
{
    /// <summary> ゲームシーンのみデバックする時はチェックをいれる/// </summary>
    [SerializeField] bool m_debugGameScene;
    [SerializeField] UISetActiveControl m_UISetActiveControl;

    [SerializeField] GameObject m_player;
    [SerializeField] GameObject m_gameOverPlayer;

    [Header("SkyBox")]
    [SerializeField] Material m_sunnySkyBox;
    [SerializeField] Material m_thunderStormSkyBox;
    [SerializeField] Material m_hurricaneSkybox;

    [Header("Audio")]
    [SerializeField] string m_GameSceneBGMName;
    [SerializeField] string m_GameClearSEName;
    [SerializeField] string m_GameOverSEName;

    [SerializeField] CinemachineVirtualCamera m_playCamera;
    [SerializeField] PlayableAsset[] m_playableAssets;
    PlayableDirector m_director;
    ScoreManager m_scoreManager;
    float m_timeLimit;

    /// <summary>ゲーム中かどうか判定する </summary>
    public bool InGame { get; set; }

    private void Start()
    {
        m_scoreManager = GetComponent<ScoreManager>();
        m_director = GetComponent<PlayableDirector>();

        m_player.SetActive(true);
        m_gameOverPlayer.SetActive(false);

        if (SoundManager.Instance)
        {
            SoundManager.Instance.PlayBGMWithFadeIn(m_GameSceneBGMName);
        }

        if (StageParent.Instance && !m_debugGameScene)
        {
            //ステージを出現させる
            StageParent.Instance.AppearanceStageObject(StageParent.Instance.GetAppearanceStagePrefab.transform);
            //制限時間をセットする
            m_timeLimit = StageParent.Instance.GetAppearanceStageData.SetTimeLimit;
            //コインの総数を数える
            m_scoreManager.CountCoinNumber();

            //skyBoxをセットする ー＞ ここ、将来的にステートパターンに
            switch (StageParent.Instance.GetAppearanceStageData.Conditons)
            {
                case StageData.WeatherConditons.Initialize:
                    break;
                case StageData.WeatherConditons.Sunny:
                    RenderSettings.skybox = m_sunnySkyBox;
                    break;
                case StageData.WeatherConditons.ThunderStorm:
                    RenderSettings.skybox = m_thunderStormSkyBox;
                    break;
                case StageData.WeatherConditons.Hurricane:
                    RenderSettings.skybox = m_hurricaneSkybox;
                    break;
                default:
                    break;
            }
        }
        m_director.playableAsset = m_playableAssets[0];
        m_director.Play();
        m_playCamera.enabled = true;
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
        if (m_debugGameScene) return;
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
            //SoundManager.Instance.StopBGMWithFadeOut(m_GameSceneBGMName, 0.1f);
            SoundManager.Instance.PlayGameSe(m_GameClearSEName,false);
        }
        //ここで演出

        //残り時間をScoreManagerに渡す
        m_scoreManager.DisplayResult(m_timeLimit);
    }

    /// <summary>
    /// タイムオーバーか、バルーンが障害物に当たったら呼ばれる
    /// </summary>
    public void GameOver()
    {
        InGame = false;

        //GameOver時の演出用のプレイヤーに切り替え、Playerの位置にセットする
        var pos = m_player.transform.position;
        m_gameOverPlayer.transform.position = pos;
        m_player.SetActive(false);
        m_gameOverPlayer.SetActive(true);
        m_director.playableAsset = m_playableAssets[1];
        m_playCamera.enabled = false;

        if (SoundManager.Instance)
        {
            //SoundManager.Instance.StopBGMWithFadeOut(m_GameSceneBGMName, 0.1f);
            SoundManager.Instance.PlayGameSe(m_GameOverSEName,false);
            m_director.Play();
        }

        if (StageParent.Instance && !m_debugGameScene)
        {
            //ステージを非表示にする
            StageParent.Instance.GetAppearanceStagePrefab.SetActive(false);
            //ステージを初期化する
            StageParent.Instance.Initialization();
        }

        if (SceneLoader.Instance)
            //タップしたらセレクト画面に戻る(タップしてください。みたいなテキストを出す)
            SceneLoader.Instance.LoadSceneWithTap(8f);

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
