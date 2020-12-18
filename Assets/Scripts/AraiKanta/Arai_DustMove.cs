using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>DustのAnimatorのSucript</summary>
public class Arai_DustMove : MonoBehaviour
{
    [Header("animationのON,OFF")]
    [SerializeField] bool dustAnim;

    Animator m_anim;

    void Start()
    {
        m_anim = this.GetComponent<Animator>();

        // DustAnimにチェックが入っているとAnimatorのパラメーター(DustMove)がtrueになる
        if (dustAnim == true)
        {
            m_anim.SetBool("DustMove", true);
        }
    }
}
