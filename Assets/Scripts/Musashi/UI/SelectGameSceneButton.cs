using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// セレクト画面のボタンにアタッチするクラス
/// </summary>
[RequireComponent(typeof(Button))]
public class SelectGameSceneButton : MonoBehaviour
{
    /// <summary>projectからPrefabをアサインすること</summary>
    [SerializeField] GameObject m_stagePrefab;
    /// <summary>天候状態をセットする</summary>
    [SerializeField] WeatherConditions m_conditions;
    /// <summary>クリアテキスト</summary>
    [SerializeField] Text m_stageClearText;
    /// <summary>次のセレクトボタン</summary>
    [SerializeField] SelectGameSceneButton m_nextSceneButton;
    SaveAndLoadWithJSON m_json;
    Button m_nextButton;

    //このままだと、m_json.CheakStageClear()が繰り返し呼ばれてしまうので修正する必要がある
    private void Start()
    {
        //まだ解放されていないステージのセレクトボタンならStart関数の処理を辞める
        if (!GetComponent<Button>().interactable)
            return;
   
        if (m_nextSceneButton && m_nextButton != this)
        {
            m_nextButton = m_nextSceneButton.GetComponent<Button>();
            m_nextButton.interactable = false;
        }

        string path = m_stagePrefab.name + "_" + m_conditions.ToString();
        m_json = new SaveAndLoadWithJSON(path);

        if (m_json.CheakStageClear())
        {
            m_stageClearText.text = "Clear!!";
            //ステージ解放
            if (m_nextButton)
            {
                m_nextButton.interactable = true;
            }
        }

        GetComponent<Button>().onClick.AddListener(() => SetStageInfo());
    }

    public void SetStageInfo()
    {
        StageParent.Instance.SetStageInfo(m_stagePrefab, m_conditions);
    }
}
