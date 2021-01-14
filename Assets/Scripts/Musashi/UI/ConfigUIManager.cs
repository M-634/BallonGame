using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// 設定画面を管理するクラス
/// </summary>
public class ConfigUIManager : SingletonMonoBehavior<ConfigUIManager>
{
    [SerializeField] string m_audioClipName;
    [SerializeField] Button m_retryBtn;
    private CanvasGroup m_canvasGroup;
    private Scene m_currentScene;

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
        //ゲームシーンなら一時停止する
        m_currentScene = SceneManager.GetActiveScene();
        if (m_currentScene.buildIndex == 2)
        {
            m_retryBtn.interactable = true;
            Time.timeScale = 0f;
            SoundManager.Instance.Pause();
        }
        else
        {
            m_retryBtn.interactable = false;
        }
    }

    public void CloseCnfigUI()
    {
        SoundManager.Instance.PlayMenuSe(m_audioClipName);
        m_canvasGroup.Hide();
        if (m_currentScene.buildIndex == 2)
        {
            Time.timeScale = 1f;
            SoundManager.Instance.Resume();
        }
    }

    public void PressRetry()
    {
        SoundManager.Instance.PlayMenuSe(m_audioClipName);
        m_canvasGroup.Hide();
        Time.timeScale = 1f;
        SoundManager.Instance.ReStart();
        SceneManager.LoadScene(m_currentScene.name);
    }

    public void QuitApplication()
    {
        SoundManager.Instance.PlayMenuSe(m_audioClipName);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
        Application.runInBackground = false;
        Application.Quit();
#endif
    }
}
