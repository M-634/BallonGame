using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using JetBrains.Annotations;

/// <summary>
/// タップを促すテキストのアニメーションを制御するクラス
/// </summary>
public class TapToLoadTextAnimation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_text;
    [SerializeField] float m_animationTime = 2f;
    [SerializeField] Color m_fadeColor;
    Sequence m_sequence;

    private void OnEnable()
    {
        m_text.SetAlpha(0f);
        m_sequence = DOTween.Sequence()
            .SetLoops(-1, LoopType.Yoyo)
            .Append(m_text.DOColor(m_fadeColor, m_animationTime).SetEase(Ease.Linear))
            .Append(m_text.DOColor(m_text.color, m_animationTime).SetEase(Ease.Linear));
        m_sequence.Play();
    }

    private void OnDisable()
    {
        m_sequence.Pause();
    }
}
