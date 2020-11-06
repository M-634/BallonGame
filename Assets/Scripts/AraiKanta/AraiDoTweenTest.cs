using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AraiDoTweenTest : MonoBehaviour
{
    /// <summary>this.transform.DOMoveの移動先の位置のX,Y,Z軸の変数</summary>
    [SerializeField] Vector3 startVector;
    /// <summary>this.transform.DOMoveの移動後の位置のX,Y,Z軸の変数</summary>
    [SerializeField] Vector3 endvector;
    /// <summary>this.transform.DOPathのObjectの通過点X,Y,Z軸の変数0</summary>
    [SerializeField] Vector3 passing0;
    /// <summary>this.transform.DOPathのObjectの通過点X,Y,Z軸の変数1</summary>
    [SerializeField] Vector3 passing1;
    /// <summary>this.transform.DOPathのObjectの通過点X,Y,Z軸の変数2</summary>
    [SerializeField] Vector3 passing2;
    /// <summary>this.transform.DOPathのObjectの通過点X,Y,Z軸の変数3</summary>
    [SerializeField] Vector3 passing3;
    /// <summary>this.transform.DOMoveの移動先の位置のX軸の変数</summary>
    [SerializeField] float startPosX;
    /// <summary>this.transform.DOMoveの移動先の位置のX軸の変数</summary>
    [SerializeField] float endPosX;
    /// <summary>this.transform.DOMoveの移動時間の変数</summary>
    [SerializeField] private float travelT;
    /// <summary>this.transform.DOMoveXの遅延時間の変数</summary>
    [SerializeField] private float DelayT;
    /// <summary>往復移動</summary>
    [SerializeField] bool roundTrip;
    /// <summary>周回移動</summary>
    [SerializeField] bool rotation;
    /// <summary>壁移動</summary>
    [SerializeField] bool moveWall;
    

    void Start()
    {
        if (roundTrip)
        {
            DOTween.Sequence()
                .Append(this.transform.DOMove(startVector, travelT).SetRelative())
                .Append(this.transform.DOMove(endvector, travelT).SetRelative())
                .SetLoops(-1)
                .Play();
        }
        if (rotation)
        {
            DOTween.Sequence()
                .Append(this.transform.DOMove(passing0, 0f))
                .Append(this.transform.DOPath(new Vector3[] { passing1, passing2, passing3, passing0 }, travelT, PathType.CatmullRom).SetEase(Ease.Linear))
                .SetLoops(-1)
                .Play();
        }
        if (moveWall)
        {
            DOTween.Sequence()
                
                .Append(this.transform.DOMoveX(startPosX, travelT).SetDelay(DelayT))
                .Append(this.transform.DOMoveX(endPosX, travelT).SetDelay(DelayT))
                .SetLoops(-1)
                .Play();
        }
    }
}
