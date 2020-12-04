using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>Dustに動きを加えるスクリプト</summary>
public class Arai_DustMove : MonoBehaviour
{
    /// <summary>this.transform.DOMoveの移動先の位置のX,Y,Z軸の変数</summary>
    [SerializeField] Vector3 startPos;
    /// <summary>this.transform.DOMoveの移動後の位置のX,Y,Z軸の変数</summary>
    [SerializeField] Vector3 endPos;
    /// <summary>this.transform.DOMoveの移動時間の変数</summary>
    [SerializeField] private float traveTime;
    void Start()
    {
        DOTween.Sequence()
               .Append(this.transform.DOMove(startPos, traveTime).SetRelative())
               .Append(this.transform.DOMove(endPos, traveTime).SetRelative())
               .SetLoops(-1)
               .Play();
    }
}
