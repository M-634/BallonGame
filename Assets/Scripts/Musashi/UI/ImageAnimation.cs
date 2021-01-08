using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIのImageにSpriteのアニメーションをさせるクラス
/// </summary>
public class ImageAnimation : MonoBehaviour
{
    [SerializeField] Sprite[] m_sprites;
    [SerializeField] int m_spritePerFrame = 6;
    [SerializeField] GameClearState m_gameClearState = default;
    public GameClearState GameClearState { get => m_gameClearState; }

    private int m_index = 0;
    private Image m_image;
    private int m_frame = 0;

    private void Awake()
    {
        m_image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        m_index = 0;
        m_frame = 0;
        StartCoroutine(AnimateSprite());
    }

    private void OnDisable()
    {
        StopCoroutine(AnimateSprite());
    }

    IEnumerator AnimateSprite()
    {
        while (true)
        {
            m_frame++;
            if (m_frame < m_spritePerFrame)
            {
                yield return null;
                continue;
            }

            m_image.sprite = m_sprites[m_index];
            m_index++;
            m_frame = 0;

            if (m_index >= m_sprites.Length) m_index = 0;
            yield return null;
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    m_frame++;

    //    if (m_frame < m_spritePerFrame) return;

    //    m_image.sprite = m_sprites[m_index];
    //    m_index++;
    //    m_frame = 0;

    //    if (m_index >= m_sprites.Length) m_index = 0;
    //}
}
