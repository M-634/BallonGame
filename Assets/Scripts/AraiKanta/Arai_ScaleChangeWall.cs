using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Arai_ScaleChangeWall : MonoBehaviour
{
    /// <summary>this.transform.DOMoveの移動時間の変数</summary>
    [SerializeField] private float traveTime;
    /// <summary>this.transform.DOMoveX & DOScale の遅延時間の変数</summary>
    [SerializeField] private float delayTime;
    /// <summary>this.transform.DOScaleのXscaleの変数</summary>
    [SerializeField] private float xScale;
    /// <summary>this.transform.DOScaleのXscaleの変数</summary>
    [SerializeField] private float x1Scale;
    void Start()
    {
        this.transform.localScale = new Vector3(0f, transform.localScale.y, transform.localScale.z);

        DOTween.Sequence()
            .Append(this.transform.DOScaleX(endValue: xScale, duration: traveTime).SetDelay(delayTime))
            .Append(this.transform.DOScaleX(endValue: x1Scale, duration: traveTime).SetDelay(delayTime))
            .SetLoops(-1)
            .Play();
    }
}
