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

    [SerializeField] string m_TitleBGMName;
    [SerializeField] string m_BtnClickClipName;

    private void Start()
    {
        if(SaveAndLoadWithJSON.IsFolderPath) 
        {
            m_beginningBtn.gameObject.SetActive(true);
            m_continueBtn.gameObject.SetActive(true);
            //m_continueBtn.interactable = true;
        }
        else
        {
            m_beginningBtn.gameObject.SetActive(false);
            m_continueBtn.gameObject.SetActive(false);
            SceneLoader.Instance.LoadSelectSceneWithTap();
            //m_continueBtn.interactable = false;
        }
        StageParent.Instance.GameClearState = GameClearState.None;
        SoundManager.Instance.PlayBGMWithFadeIn(m_TitleBGMName);
    }

    /// <summary>
    /// はじめから（SaveDataを消す）
    /// </summary>
    public void OnBeginningBtn()
    {
        SoundManager.Instance.PlaySe(m_BtnClickClipName);
        SaveAndLoadWithJSON.DeleteSaveData();
        SceneLoader.Instance.LoadSelectScene();
    }

    /// <summary>
    /// 続きから
    /// </summary>
    public void OnContinueBtn()
    {
        SoundManager.Instance.PlaySe(m_BtnClickClipName);
        SceneLoader.Instance.LoadSelectScene();
    }
}
