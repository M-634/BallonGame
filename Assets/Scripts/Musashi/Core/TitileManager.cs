using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイトルシーンを管理するクラス
/// </summary>
public class TitileManager : MonoBehaviour
{
    [SerializeField] Button m_beginningBtn;
    [SerializeField] Button m_continueBtn;

    private void Start()
    {
        if(SaveAndLoadWithJSON.IsFolderPath) 
        {
            m_continueBtn.interactable = true;
        }
        else
        {
            m_continueBtn.interactable = false;
        }
        StageParent.Instance.GameClearState = GameClearState.None;
    }

    /// <summary>
    /// はじめから（SaveDataを消す）
    /// </summary>
    public void OnBeginningBtn()
    {
        SaveAndLoadWithJSON.DeleteSaveData();
        SceneLoader.Instance.LoadSelectScene();
    }

    /// <summary>
    /// 続きから
    /// </summary>
    public void OnContinueBtn()
    {
        SceneLoader.Instance.LoadSelectScene();
    }
}
