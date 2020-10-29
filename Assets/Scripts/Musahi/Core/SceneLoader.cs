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

    private void Start()
    {
        m_tapToLoadText.gameObject.SetActive(false);
    }
    public void Load(string loadSceneName)
    {
        m_currentLoadCorutine = LoadWithCorutine(loadSceneName);
        StartCoroutine(m_currentLoadCorutine);
    }

    public void LoadAddtive(string loadSceneNamde)
    {
        m_currentLoadCorutine = LoadAddtiveCorutine(loadSceneNamde);
        StartCoroutine(m_currentLoadCorutine);
    }

    public void LoadWithTap(string loadSceneName)
    {
        m_currentLoadCorutine = LoadWithTapCorutine(loadSceneName);
        StartCoroutine(m_currentLoadCorutine);
    }

    private IEnumerator LoadWithTapCorutine(string loadSceneName)
    {
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

        AsyncOperation async = SceneManager.LoadSceneAsync(loadSceneName, LoadSceneMode.Single);

        while (async.progress < 0.99f)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator LoadAddtiveCorutine(string loadSceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(loadSceneName, LoadSceneMode.Additive);

        while (async.progress < 0.99f)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator LoadWithCorutine(string loadSceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(loadSceneName, LoadSceneMode.Single);

        while (async.progress < 0.99f)
        {
            yield return new WaitForEndOfFrame();
        }
    }
}
