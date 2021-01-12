using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Rigth,LeftWall　のスクリプト
/// </summary>
public class Arai_MoveWall : MonoBehaviour
{
    [Header("最初の移動先")]
    /// <summary>this.transform.DOMoveの移動先の位置のX軸の変数</summary>
    [SerializeField] private float startPosX;
    [Header("最初から反対方向への移動先")]
    /// <summary>this.transform.DOMoveの移動先の位置のX軸の変数</summary>
    [SerializeField] private float endPosX;
    [Header("移動時間")]
    /// <summary>this.transform.DOMoveの移動時間の変数</summary>
    [SerializeField] private float traveTime;
    [Header("遅延時間")]
    /// <summary>this.transform.DOMoveX & DOScale の遅延時間の変数</summary>
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
