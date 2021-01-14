using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// セレクト画面のボタンにアタッチするクラス
/// ハイライト時に、ステージの動画とクリアスコア、クリアタイム,ステージの名前
/// を表示する
/// </summary>
[RequireComponent(typeof(Button))]
public class SelectStageButton : MonoBehaviour, IPointerEnterHandler
{
    [Header("フィールド")]
    [SerializeField] VideoClip m_videoClip;//ここけす
    [SerializeField] VideoPlayer m_videoPlayer;
    [SerializeField] Image m_stageClearImage;
    [SerializeField] Sprite m_unOpenedSprite;
    [SerializeField] Sprite m_openedSprite;
    [SerializeField] int m_stageNumber;

    [Header("各テキストUI")]
    [SerializeField] TextMeshProUGUI m_clearTimeText;
    [SerializeField] TextMeshProUGUI m_clearScoreText;
    [SerializeField] TextMeshProUGUI m_stageNameText;

    public int StageNumber { get => m_stageNumber;}
    public Image StageClearImage { get => m_stageClearImage; }
    
    private StageData m_stageData;
    public StageData StageData { get => m_stageData; set => m_stageData = value;}
    private Button m_button;
    public Button SelectButton
    {
        get
        {
            if (m_button) return m_button;

            m_button = GetComponent<Button>();
            return m_button;
        }
    }

    private Image m_image;

    public Image Image
    {
        get
        {
            if (m_image) return m_image;
            m_image = GetComponent<Image>();
            return Image;
        }
    }
    
    public void SetOpenedStageSprite()
    {
        Image.sprite = m_openedSprite;
    }

    public void SetUnOpenedStageSprite()
    {
        Image.sprite = m_unOpenedSprite;
    }



    private void SetStageInfo()
    {
        if (StageParent.Instance)
            StageParent.Instance.SetStageInfo(m_stageNumber);
    }

    private void ShowStageInfo()
    {
        //各種UIへ情報をセットする
        m_stageNameText.text = StageData.StagePrefab.name;
        m_clearScoreText.text = "ClearScore:" +  StageData.HighScore.ToString();
        m_clearTimeText.TimerInfo(StageData.ClearTime,"ClearTime:");

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
