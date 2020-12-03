using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// セレクト画面のボタンにアタッチするクラス
/// ハイライト時に、ステージの動画とクリアスコア、クリアタイム,ステージの名前
/// を表示する
/// </summary>
[RequireComponent(typeof(Button))]
public class SelectGameSceneButton : MonoBehaviour, IPointerEnterHandler
{
    [Header("StagePrefabをアサインしてね！")]
    [SerializeField] GameObject m_stagePrefab;

    [Header("各テキストUI")]
    [SerializeField] Text m_stageClearText;
    [SerializeField] Text m_clearTimeText;
    [SerializeField] Text m_clearScoreText;
    [SerializeField] Text m_stageNameText;

    public Text ClearText { get => m_stageClearText; }
    public StageData StageData { get; set; }
    public GameObject StagePrefab { get => m_stagePrefab; }
    private Button m_button;
    public Button SelectButton
    {
        get
        {
            if (m_button == null)
            {
                m_button = GetComponent<Button>();
                return m_button;
            }
            return m_button;
        }
    }

    private void Start()
    {
        SelectButton.onClick.AddListener(() => SetStageInfo());
    }

    private void SetStageInfo()
    {
        if (StageParent.Instance)
            StageParent.Instance.SetStageInfo(m_stagePrefab);
    }

    private void ShowStageInfo()
    {
        m_stageNameText.text = StageData.StagePrefab.name;
        m_clearScoreText.text = StageData.HighScore.ToString();
        m_clearTimeText.TimerInfo(StageData.ClearTime);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (SelectButton.interactable)
        {
            ShowStageInfo();
        }
    }
}
