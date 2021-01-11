using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲーム中、ゲームクリア、ゲームオーバー時のUIをまとめるクラス
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

    [Header("タイマーテキスト")]
    [SerializeField] TextMeshProUGUI m_timerText;

    [Header("スコアテキスト")]
    [SerializeField] TextMeshProUGUI m_currentScoreText;

    [Header("リザルトテキスト")]
    [SerializeField] TextMeshProUGUI m_clearTimeScoreText;
    [SerializeField] TextMeshProUGUI m_resulScoreText;

    [Header("RankSprites")]
    [SerializeField] Sprite m_rankImageA;
    [SerializeField] Sprite m_rankImageB;
    [SerializeField] Sprite m_rankImageC;

    [SerializeField] Image m_resultRankImage;

    public GameObject GameSceneUI { get => m_gameSceneUI.gameObject; }
    public GameObject GameOverUI { get => m_GameOverUI.gameObject; }
    public GameObject GameClearUI { get => m_GameClearUI.gameObject; }
    public TextMeshProUGUI TimerText { get => m_timerText; }
    public TextMeshProUGUI CurrentScoreText { get => m_currentScoreText; }
    public TextMeshProUGUI ClearTimeScoreText { get => m_clearTimeScoreText; }
    public TextMeshProUGUI ResulScoreText { get => m_resulScoreText; }
 

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
        m_startCountDown.FadeOutWithDoTween(2f, () => m_eventSystemInGameScene.ExecuteGameStartEvent());
    }

    /// <summary>
    /// コインの総数と獲得数の比率でランクを決定する
    /// ランクはA～C
    /// </summary>
    public void DetermineTheRank(int raito)
    {
        if(raito > 90)
        {
            m_resultRankImage.sprite = m_rankImageA;
        }
        else if(raito > 50)
        {
            m_resultRankImage.sprite = m_rankImageB;
        }
        else
        {
            m_resultRankImage.sprite = m_rankImageC;
        }
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
