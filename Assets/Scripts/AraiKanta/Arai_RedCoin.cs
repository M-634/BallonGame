using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// RedCoin の スクリプト
/// </summary>
public class Arai_RedCoin : MonoBehaviour
{
    [SerializeField] private float rotationTime;
    GameObject obj;
    void Start()
    {
        obj = GameObject.Find("RedCoin");
        
        DOTween.Sequence();
        transform.DOLocalRotate(new Vector3(0, 360f, 0), rotationTime, RotateMode.FastBeyond360)
             .SetEase(Ease.Linear)
             .SetLoops(-1, LoopType.Restart);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // ゲームオブジェクトを非表示にする
            gameObject.SetActive(false);
            Debug.Log("Coin消えた");
        }
    }
}
