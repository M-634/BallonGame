using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class otsuki_Coin : MonoBehaviour
{
    [SerializeField] private float rotationTime;
    GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.Find("RedCoin");

        DOTween.Sequence();
        transform.DOLocalRotate(new Vector3(0, 360f, 0), rotationTime, RotateMode.FastBeyond360)
             .SetEase(Ease.Linear)
             .SetLoops(-1, LoopType.Restart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
