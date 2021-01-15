using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// セレクトシーン内のロードボタン用のスクリプト
/// </summary>
public class LoadSceneButtonInSelect : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_stageNameText;

    public void LoadTitleScene()
    {
        SceneLoader.Instance.LoadTitleScene();
    }

    public void LoadGameScene()
    {
        if (StageParent.Instance.GetAppearanceStageData == null
            || StageParent.Instance.GetAppearanceStagePrefab == null)
        {
            m_stageNameText.text = "Stageを選択してね！！";
            return;
        }
        SoundManager.Instance.PlayMenuSe("StartGameBtn");
        SceneLoader.Instance.LoadGameScene();
    }
}
