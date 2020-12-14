using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// セレクトシーン内のロードボタン用のスクリプト
/// </summary>
public class LoadSceneButtonInSelect : MonoBehaviour
{
    [SerializeField] Text m_stageNameText;

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
        SceneLoader.Instance.LoadGameScene();
    }
}
