using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_Btn_Musashi : MonoBehaviour
{
    [SerializeField] Image m_fadeImage;
    [SerializeField] float m_fadeTime;
    [SerializeField] string m_loadSceneName;


    public void OnClickBtn()
    {
        m_fadeImage.SetAlpha(1f);
        SceneLoader.Instance.LoadAddtiveWithCallback(m_loadSceneName,() =>StartCoroutine(m_fadeImage.Fadeout(m_fadeTime)));

    }
}
