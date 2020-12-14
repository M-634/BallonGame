using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

/// <summary>
/// セレクト画面のボタンにアタッチするクラス
/// ハイライト時に、ステージの動画とクリアスコア、クリアタイム,ステージの名前
/// を表示する
/// </summary>
[RequireComponent(typeof(Button))]
public class SelectStageButton : MonoBehaviour, IPointerEnterHandler
{
    [Header("フィールド")]
    [SerializeField] GameObject m_stagePrefab;
    [SerializeField] VideoClip m_videoClip;
    [SerializeField] VideoPlayer m_videoPlayer;

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


    //private void Start()
    //{
    //    SelectButton.onClick.AddListener(() => SetStageInfo());
    //}

    private void SetStageInfo()
    {
        if (StageParent.Instance)
            StageParent.Instance.SetStageInfo(m_stagePrefab);
    }

    private void ShowStageInfo()
    {
        //各種UIへ情報をセットする
        m_stageNameText.text = StageData.StagePrefab.name;
        m_clearScoreText.text = StageData.HighScore.ToString();
        m_clearTimeText.TimerInfo(StageData.ClearTime);

        //動画再生
        m_videoPlayer.source = VideoSource.VideoClip;
        m_videoPlayer.clip = m_videoClip;
        m_videoPlayer.isLooping = true;
        m_videoPlayer.Play();

        //ステージ情報をセットする
        SetStageInfo();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (SelectButton.interactable)
        {
            ShowStageInfo();
        }
    }
}
