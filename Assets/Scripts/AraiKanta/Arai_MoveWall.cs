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
    /// <summary>this.transform.DOMoveXの遅延時間の変数</summary>
    [SerializeField] private float delayTime;

    void Start()
    {
        DOTween.Sequence()
            .Append(this.transform.DOMoveX(startPosX, traveTime).SetDelay(delayTime))
            .Append(this.transform.DOMoveX(endPosX, traveTime).SetDelay(delayTime))
            .SetLoops(-1)
            .Play();
    }
}
