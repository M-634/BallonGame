using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PropellerSeya : MonoBehaviour
{
    [SerializeField, Range(0f, 50f)] float ratateTime = 6f;
    void Start()
    {
        transform.DORotate(new Vector3(0f, 360f, 0f), ratateTime, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
}
