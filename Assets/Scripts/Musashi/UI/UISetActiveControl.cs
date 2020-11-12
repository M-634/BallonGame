using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームシーン内のUIのアクティブをコントロールするクラス
/// </summary>
public class UISetActiveControl : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Canvas m_gameSceneUI;
    [SerializeField] Canvas m_GameOverUI;
    [SerializeField] Canvas m_GameClearUI;

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
}
