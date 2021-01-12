using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// RedCoin の スクリプト
/// </summary>
public class Arai_Coin : MonoBehaviour
{
    [Header("回転時間")]
    /// <summary>n秒に360度回転するかの時間</summary>
    [SerializeField] private float rotationTime = 3f;
    [Header("遅延時間")]
    /// <summary>コインが回転をはじめるまでの時間</summary>
    [SerializeField] private float delayTime;
    GameObject obj;
    void Start()
    {
        obj = GameObject.Find("RedCoin");

        delayTime = Random.Range(1f, 2f);
        
        DOTween.Sequence();
        transform.DOLocalRotate(new Vector3(0, 360f, 0), rotationTime, RotateMode.FastBeyond360)
             .SetDelay(delayTime)
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
