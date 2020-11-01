using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
///非同期処理を使ったシーンロード 
/// </summary>
public class SceneLoader : SingletonMonoBehavior<SceneLoader>
{
    private IEnumerator m_currentLoadCorutine;
    [SerializeField] Text m_tapToLoadText;

    [Header("この変数にアタッチするCanvasのSortOrderを１にすること")]
    [SerializeField] Canvas m_loadCanvas;
    [SerializeField] Image m_fadeImage;
    [SerializeField] float m_fadeOutTime;
    [SerializeField] float m_fadeInTime;

    private void Start()
    {
        m_tapToLoadText.gameObject.SetActive(false);
        m_loadCanvas.enabled = false;
    }

    /// <summary>
    /// セレクトボタンを押したらロードする時に使う関数
    /// </summary>
    /// <param name="loadSceneName">ゲームシーン</param>
    public void Load(string loadSceneName)
    {
        m_currentLoadCorutine = LoadWithCorutine(loadSceneName);
        StartCoroutine(m_currentLoadCorutine);
    }

    private IEnumerator LoadWithCorutine(string loadSceneName)
    {
        m_loadCanvas.enabled = true;
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

    /// <summary>
    /// 画面をタップしたらロードする関数
    /// </summary>
    /// <param name="selectSceneName">タイトル→セレクトシーン,ゲームシーン→セレクトシーン</param>
    public void LoadWithTap(string selectSceneName)
    {
        m_currentLoadCorutine = LoadWithTapCorutine(selectSceneName);
        StartCoroutine(m_currentLoadCorutine);
    }

    private IEnumerator LoadWithTapCorutine(string loadSceneName)
    {
        m_loadCanvas.enabled = true;
        m_tapToLoadText.gameObject.SetActive(true);
        while (true)
        {
#if UNITY_ANDROID
#else
            if (Input.GetMouseButtonDown(0))
            {
                m_tapToLoadText.gameObject.SetActive(false);
                break;
            }
#endif
            yield return null;
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
