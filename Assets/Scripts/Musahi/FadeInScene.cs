using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 各シーンの始めにフェードインするクラス
/// </summary>
[RequireComponent(typeof(Image))]
public class FadeInScene : MonoBehaviour
{
    [SerializeField] Image m_fadeImage;
    [SerializeField] float m_fadeInTime = 2f;
    [SerializeField,Range(0.5f,1f)] float m_setAlpha = 1f;

    // Start is called before the first frame update
    void Start()
    {
        m_fadeImage.enabled = true;
        m_fadeImage.SetAlpha(m_setAlpha);
        StartCoroutine(m_fadeImage.FadeOut(m_fadeInTime, () => m_fadeImage.enabled = false)) ;
    }
}
