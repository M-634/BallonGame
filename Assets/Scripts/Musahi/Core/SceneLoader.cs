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

    public void LoadAddtiveWithCallback(string sceneName,UnityAction callback = null)
    {
        m_currentLoadCorutine = LoadAddtiveWithCallbackCorutine(sceneName, callback);
        StartCoroutine(m_currentLoadCorutine);
    }

    private IEnumerator LoadAddtiveWithCallbackCorutine(string sceneName,UnityAction callback = null)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        async.allowSceneActivation = true;

        while (async.progress < 0.99f)
        {
            yield return new WaitForEndOfFrame();
        }

        callback?.Invoke();
    }
}
