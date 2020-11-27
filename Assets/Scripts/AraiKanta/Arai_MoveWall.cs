using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Rigth,LeftWall　のスクリプト
/// </summary>
public class Arai_MoveWall : MonoBehaviour
{
    /// <summary>this.transform.DOMoveの移動先の位置のX軸の変数</summary>
    [SerializeField] private float startPosX;
    /// <summary>this.transform.DOMoveの移動先の位置のX軸の変数</summary>
    [SerializeField] private float endPosX;
    /// <summary>this.transform.DOMoveの移動時間の変数</summary>
    [SerializeField] private float traveTime;
    /// <summary>this.transform.DOMoveX & DOScale の遅延時間の変数</summary>
    [SerializeField] private float delayTime;
    /// <summary>this.transform.DOScaleのXscaleの変数</summary>
    [SerializeField] private float xScale;
    /// <summary>this.transform.DOScaleのXscaleの変数</summary>
    [SerializeField] private float x1Scale;

    /// <summary>動く壁</summary>
    [SerializeField] bool moveWall;
    /// <summary>大きさ変わる壁</summary>
    [SerializeField] bool scale;

    void Start()
    {
        //動く壁
        if (moveWall)
        {
            DOTween.Sequence()
            .Append(this.transform.DOMoveX(startPosX, traveTime).SetDelay(delayTime))
            .Append(this.transform.DOMoveX(endPosX, traveTime).SetDelay(delayTime))
            .SetLoops(-1)
            .Play();
        }

        //一応Sucaleを変更できるように
        if (scale)
        {
            this.transform.localScale = new Vector3(0f, transform.localScale.y, transform.localScale.z);

            DOTween.Sequence()
                .Append(this.transform.DOScaleX(endValue: xScale, duration: traveTime).SetDelay(delayTime))
                .Append(this.transform.DOScaleX(endValue: x1Scale, duration: traveTime).SetDelay(delayTime))
                .SetLoops(-1)
                .Play();
        }
    }
}
