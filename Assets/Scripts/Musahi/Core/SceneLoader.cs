using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
///非同期処理を使ったシーンロード 
/// </summary>
public class SceneLoader : SingletonMonoBehavior<SceneLoader>
{
    [Header("ロードシーンの名前を設定すること")]
    [SerializeField] string m_loadTitleSceneName;
    [SerializeField] string m_loadSelectSceneName;
    [SerializeField] string m_loadGameSceneName;

    [Header("UI")]
    [SerializeField] Text m_tapToLoadText;
    [SerializeField] Canvas m_loadCanvas;
    [SerializeField] Image m_fadeImage;
    [SerializeField] float m_fadeOutTime;
    [SerializeField] float m_fadeInTime;

    private IEnumerator m_currentLoadCorutine;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        m_tapToLoadText.gameObject.SetActive(false);
        m_loadCanvas.sortingOrder = 1;//描画を最前列にするため
        m_loadCanvas.enabled = false;
    }

    public void LoadGameScene()
    {
        m_currentLoadCorutine = LoadGameSceneWithCorutine();
        StartCoroutine(m_currentLoadCorutine);
    }

    private IEnumerator LoadGameSceneWithCorutine()
    {
        m_loadCanvas.enabled = true;
        StartCoroutine(m_fadeImage.FadeIn(m_fadeOutTime));
        yield return new WaitForSeconds(m_fadeOutTime);
        AsyncOperation async = SceneManager.LoadSceneAsync(m_loadGameSceneName, LoadSceneMode.Single);

        while (async.progress < 0.99f)
        {
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(m_fadeImage.FadeOut(m_fadeInTime));
        yield return new WaitForSeconds(m_fadeInTime);
        m_loadCanvas.enabled = false;
    }

   
    public void LoadTitleSceneWithTap()
    {
        m_currentLoadCorutine = LoadWithTapCorutine(m_loadTitleSceneName);
        StartCoroutine(m_currentLoadCorutine);
    }

    public void LoadSelectSceneWithTap()
    {
        m_currentLoadCorutine = LoadWithTapCorutine(m_loadSelectSceneName);
        StartCoroutine(m_currentLoadCorutine);
    }

    private IEnumerator LoadWithTapCorutine(string loadSceneName)
    {
        m_loadCanvas.enabled = true;
        m_tapToLoadText.gameObject.SetActive(true);

        while (true)
        {
#if UNITY_ANDROID
            break;
#else 
            if (Input.GetMouseButtonDown(0))
            {
                m_tapToLoadText.gameObject.SetActive(false);
                break;
            }
            yield return null;
#endif
        }
        
        StartCoroutine(m_fadeImage.FadeIn(m_fadeOutTime));
        yield return new WaitForSeconds(m_fadeOutTime);
        AsyncOperation async = SceneManager.LoadSceneAsync(loadSceneName, LoadSceneMode.Single);

        while (async.progress < 0.99f)
        {
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(m_fadeImage.FadeOut(m_fadeInTime));
        yield return new WaitForSeconds(m_fadeInTime);
        m_loadCanvas.enabled = false;
    }
}
