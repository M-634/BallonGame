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

    public void Load(string loadSceneName)
    {
        m_currentLoadCorutine = LoadWithCorutine(loadSceneName);
        StartCoroutine(m_currentLoadCorutine);
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
