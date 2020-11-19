using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AraiTest : MonoBehaviour
{ 
    void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 360f, 0), 6f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
}
