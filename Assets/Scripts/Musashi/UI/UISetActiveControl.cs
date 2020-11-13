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
    [SerializeField] int m_contDown = 3;

    [Header("スコアテキスト")]
    [SerializeField] Text m_currentScoreText;

    [Header("リザルトテキスト")]
    [SerializeField] Text m_leftTimeScoreText;
    [SerializeField] Text m_getScoreText;
    [SerializeField] Text m_totalScoreText;

    public GameObject GameSceneUI { get => m_gameSceneUI.gameObject; }
    public GameObject GameOverUI { get => m_GameOverUI.gameObject; }
    public GameObject GameClearUI { get => m_GameClearUI.gameObject; }
    public Text CurrentScoreText { get => SetActiveCanvasWithText(GameSceneUI, m_currentScoreText); }
    public Text LeftTimeScoreText { get => SetActiveCanvasWithText(GameClearUI, m_leftTimeScoreText); }
    public Text GetScoreText { get => SetActiveCanvasWithText(GameClearUI, m_getScoreText); }
    public Text TotalScoreText { get => SetActiveCanvasWithText(GameClearUI, m_totalScoreText); }


    private Text SetActiveCanvasWithText(GameObject canvas, Text text)
    {
        if (canvas.activeSelf == false)
        {
            canvas.SetActive(true);
        }

        if (text.gameObject.activeSelf == false)
        {
            text.gameObject.SetActive(true);
        }
        return text;
    }


    private void Start()
    {
        InisitializeUISetAcitve();
        //3,2,1スタート！！
        StartCoroutine(StartCountDownCorutine());
    }

    IEnumerator StartCountDownCorutine()
    {
        while (m_contDown > 0)
        {
            m_startCountDown.text = m_contDown.ToString();
            yield return new WaitForSeconds(1f);
            m_contDown--;
        }

        m_startCountDown.text = "START!!";
        yield return null;
        m_startCountDown.gameObject.SetActive(false);
        m_eventSystemInGameScene.ExecuteGameStartEvent();
    }

    public void InisitializeUISetAcitve()
    {
        GameSceneUI.SetActive(true);
        GameOverUI.SetActive(false);
        GameClearUI.SetActive(false);
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
