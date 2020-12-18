using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ThornEnemyObject のスクリプト
/// </summary>
public class Arai_ThornEnemyObject : MonoBehaviour
{
    [Header("最初に動かす位置")]
    /// <summary>this.transform.DOMoveの移動先の位置のX,Y,Z軸の変数</summary>
    [SerializeField] Vector3 startPos;
    [Header("最初から反対方向に動かす位置")]
    /// <summary>this.transform.DOMoveの移動後の位置のX,Y,Z軸の変数</summary>
    [SerializeField] Vector3 endPos;
    [Header("1番最初の通過点")]
    /// <summary>this.transform.DOPathのObjectの通過点X,Y,Z軸の変数0</summary>
    [SerializeField] Vector3 passing0;
    [Header("2番最初の通過点")]
    /// <summary>this.transform.DOPathのObjectの通過点X,Y,Z軸の変数1</summary>
    [SerializeField] Vector3 passing1;
    [Header("3番最初の通過点")]
    /// <summary>this.transform.DOPathのObjectの通過点X,Y,Z軸の変数2</summary>
    [SerializeField] Vector3 passing2;
    [Header("4番最初の通過点")]
    /// <summary>this.transform.DOPathのObjectの通過点X,Y,Z軸の変数3</summary>
    [SerializeField] Vector3 passing3;
    [Header("移動時間")]
    /// <summary>this.transform.DOMoveの移動時間の変数</summary>
    [SerializeField] private float traveTime;
    [Header("往復移動")]
    /// <summary>往復移動</summary>
    [SerializeField] bool roundTrip;
    [Header("周回移動")]
    /// <summary>周回移動</summary>
    [SerializeField] bool rotation;

    void Start()
    {
        //往復移動
        if (roundTrip)
        {
            DOTween.Sequence()
                .Append(this.transform.DOMove(startPos, traveTime).SetRelative())
                .Append(this.transform.DOMove(endPos, traveTime).SetRelative())
                .SetLoops(-1)
                .Play();
        }
        //周回移動
        if (rotation)
        {
            DOTween.Sequence()
                .Append(this.transform.DOMove(passing0, 0f))
                .Append(this.transform.DOPath(new Vector3[] { passing1, passing2, passing3, passing0 }, traveTime, PathType.CatmullRom).SetEase(Ease.Linear))
                .SetLoops(-1)
                .Play();
        }
    }
}
