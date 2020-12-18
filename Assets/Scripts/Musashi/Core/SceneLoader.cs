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
        m_loadCanvas.enabled = true;
        m_fadeImage.raycastTarget = false;
    }

    public void LoadGameScene()
    {
        m_currentLoadCorutine = LoadGameSceneWithCorutine();
        StartCoroutine(m_currentLoadCorutine);
    }

    private IEnumerator LoadGameSceneWithCorutine()
    {
        //m_loadCanvas.enabled = true;
        m_fadeImage.raycastTarget = true;
        //StartCoroutine(m_fadeImage.FadeIn(m_fadeOutTime));
        m_fadeImage.FadeInWithDoTween(m_fadeOutTime);
        yield return new WaitForSeconds(m_fadeOutTime);

        AsyncOperation async = SceneManager.LoadSceneAsync(m_loadGameSceneName, LoadSceneMode.Single);
    
        while (async.progress < 0.99f)
        {
            yield return null;
        }
        Debug.Log("load complete!");
        m_fadeImage.FadeOutWithDoTween(m_fadeInTime, () => m_fadeImage.raycastTarget = false);
        yield return new WaitForSeconds(m_fadeInTime);

        //ゲームシーン開始前は、TimeLineが実行される！！今は、仮でカウントダウンしている
        var countDownUI = GameObject.FindGameObjectWithTag("StageManager").GetComponent<UISetActiveControl>();
        if (countDownUI)
        {
            StartCoroutine(countDownUI.StartCountDownCorutine());
        }
        else
        {
            Debug.LogError("StageMagerにUISetActiveControlコンポーネントがアタッチされていません！！");
        }
    }

    /// <summary>
    /// GameScene → SelectScene
    /// </summary>
    public void LoadSelectSceneWithTap()
    {
        m_currentLoadCorutine = LoadWithTapCorutine(m_loadSelectSceneName);
        StartCoroutine(m_currentLoadCorutine);
    }

    private IEnumerator LoadWithTapCorutine(string loadSceneName)
    {
        Debug.Log("ypba");
        //m_loadCanvas.enabled = true;
        m_fadeImage.raycastTarget = true;
        m_tapToLoadText.gameObject.SetActive(true);

        while (true)
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    break;
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                break;
            }
            yield return null;
        }
        m_tapToLoadText.gameObject.SetActive(false);
        //StartCoroutine(m_fadeImage.FadeIn(m_fadeOutTime));
        m_fadeImage.FadeInWithDoTween(m_fadeOutTime);
        //yield return new WaitForSeconds(m_fadeOutTime);
        AsyncOperation async = SceneManager.LoadSceneAsync(loadSceneName, LoadSceneMode.Single);

        while (async.progress < 0.99f && m_fadeImage.color.a < 1f)
        {
            yield return null;
            Debug.Log(m_fadeImage.color.a);
        }
        //StartCoroutine(m_fadeImage.FadeOut(m_fadeInTime, () => m_loadCanvas.enabled = false));
        m_fadeImage.FadeOutWithDoTween(m_fadeInTime, () => m_fadeImage.raycastTarget = false);
    }

    public void LoadTitleScene()
    {
        m_currentLoadCorutine = LoadScene(m_loadTitleSceneName);
        StartCoroutine(m_currentLoadCorutine);
    }

    public void LoadSelectScene()
    {
        m_currentLoadCorutine = LoadScene(m_loadSelectSceneName);
        StartCoroutine(m_currentLoadCorutine);
    }

    /// <summary>
    /// デフォルト
    /// </summary>
    /// <param name="loadSceneName"></param>
    /// <returns></returns>
    private IEnumerator LoadScene(string loadSceneName)
    {
        //m_loadCanvas.enabled = true;
        m_fadeImage.raycastTarget = true;
        m_fadeImage.FadeInWithDoTween(m_fadeOutTime);
        //StartCoroutine(m_fadeImage.FadeIn(m_fadeOutTime));
        //yield return new WaitForSeconds(m_fadeOutTime);
        AsyncOperation async = SceneManager.LoadSceneAsync(loadSceneName, LoadSceneMode.Single);

        while (async.progress < 0.99f && m_fadeImage.color.a < 1f)
        {
            yield return null;
            Debug.Log(m_fadeImage.color.a);
        }
        //StartCoroutine(m_fadeImage.FadeOut(m_fadeInTime, () => m_loadCanvas.enabled = false));
        m_fadeImage.FadeOutWithDoTween(m_fadeInTime, () => m_fadeImage.raycastTarget = false);
    }
}


