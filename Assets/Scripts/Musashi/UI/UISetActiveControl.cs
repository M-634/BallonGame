using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームシーン内のUIのアクティブをコントロールするクラス
/// </summary>
public class UISetActiveControl : EventReceiver<UISetActiveControl>
{
    [Header("UI")]
    [SerializeField] Canvas m_gameSceneUI;
    [SerializeField] Canvas m_GameOverUI;
    [SerializeField] Canvas m_GameClearUI;

    [Header("スタート時のカウントダウン")]
    [SerializeField] Text m_startCountDown;
    [SerializeField] int m_countDown = 3;

    [Header("スコアテキスト")]
    [SerializeField] Text m_currentScoreText;

    [Header("リザルトテキスト")]
    [SerializeField] Text m_leftTimeScoreText;
    [SerializeField] Text m_getResulScoreText;
    [SerializeField] Text m_totalScoreText;

    public GameObject GameSceneUI { get => m_gameSceneUI.gameObject; }
    public GameObject GameOverUI { get => m_GameOverUI.gameObject; }
    public GameObject GameClearUI { get => m_GameClearUI.gameObject; }
    public Text CurrentScoreText { get => m_currentScoreText; }
    public Text LeftTimeScoreText { get => m_leftTimeScoreText; }
    public Text GetResulScoreText { get => m_getResulScoreText; }
    public Text TotalScoreText { get => m_totalScoreText; }

    /// <summary> ゲームシーンのみデバックする時はチェックをいれる/// </summary>
    [SerializeField] bool m_debugGameScene;

    private void Start()
    {
        InisitializeUISetAcitve();
#if UNITY_EDITOR
        if (m_debugGameScene)
        {
            //3,2,1スタート！！
            StartCoroutine(StartCountDownCorutine());
        }
#endif
    }

    /// <summary>
    /// SceneLoaderから呼ばれる。
    /// シーンがロードされ、FadeImageがフェードアウトした後に
    /// カウントダウンが開始される。
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartCountDownCorutine()
    {
        m_startCountDown.SetAlpha(1f);
        while (m_countDown > 0)
        {
            m_startCountDown.text = m_countDown.ToString();
            yield return new WaitForSeconds(1f);
            m_countDown--;
        }

        m_startCountDown.text = "START!!";
        yield return null;
        //StartCoroutine(m_startCountDown.FadeOut(2f, () => m_eventSystemInGameScene.ExecuteGameStartEvent()));
    }

    public void InisitializeUISetAcitve()
    {
        GameSceneUI.SetActive(true);
        GameOverUI.SetActive(false);
        GameClearUI.SetActive(false);
        CurrentScoreText.text = "Score:";
        m_startCountDown.SetAlpha(0f);
    }

    public void UISetActiveWithGameOver()
    {
        GameSceneUI.SetActive(false);
        GameOverUI.SetActive(true);
    }

    public void UISetActiveWithGameClear()
    {
        GameSceneUI.SetActive(false);
        GameClearUI.SetActive(true);
    }

    protected override void OnEnable()
    {
        m_eventSystemInGameScene.GameClearEvent += UISetActiveWithGameClear;
        m_eventSystemInGameScene.GameOverEvent += UISetActiveWithGameOver;
    }

    protected override void OnDisable()
    {
        m_eventSystemInGameScene.GameClearEvent -= UISetActiveWithGameClear;
        m_eventSystemInGameScene.GameOverEvent -= UISetActiveWithGameOver;
    }
}
