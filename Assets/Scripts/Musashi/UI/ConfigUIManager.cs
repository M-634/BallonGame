using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 設定画面を管理するクラス
/// </summary>
public class ConfigUIManager :SingletonMonoBehavior<ConfigUIManager> 
{
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
        m_canvasGroup.Show();
    }

    public void CloseCnfigUI()
    {
        m_canvasGroup.Hide();
    }
}
