using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet_otsuki : MonoBehaviour
{
    [Header("発射地点")]
    public GameObject setPos;
    [Header("目標地点(弾着今)")]
    [SerializeField] Vector3 target;
    [Header("装填にかかる時間")]
    [SerializeField] float waitTime;
    [Header("目標到達時間")]
    [SerializeField] float arrivalTime;
    void Start()
    {
        DOTween.Sequence()
            .Append(this.transform.DOMove(setPos.transform.position, 0f))
            .Join(this.transform.DOScale(new Vector3(0, 0, 0), 0f))
            .Append(this.transform.DOMove(target, arrivalTime).SetEase(Ease.Linear).SetRelative().SetDelay(waitTime))
            .Join(this.transform.DOScale(new Vector3(1, 1, 1), 0f))
            .SetLoops(-1)
            .Play();
    }
}
