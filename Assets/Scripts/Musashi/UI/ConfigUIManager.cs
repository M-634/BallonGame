using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 設定画面を管理するクラス
/// </summary>
public class ConfigUIManager :SingletonMonoBehavior<ConfigUIManager> 
{
    [SerializeField] string m_audioClipName;
    private CanvasGroup m_canvasGroup;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
        m_canvasGroup.Hide();
    }

    public void ShowConfigUI()
    {
        SoundManager.Instance.PlayMenuSe(m_audioClipName);
        m_canvasGroup.Show();
    }

    public void CloseCnfigUI()
    {
        SoundManager.Instance.PlayMenuSe(m_audioClipName);
        m_canvasGroup.Hide();
    }
}
