using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 加速ギミックに取り付ける。プレイヤーを指定した高さに上げたり下げたりする。
/// </summary>
public class PlayerOnStream : MonoBehaviour
{
    /// <summary>加速ギミックを抜けた後の特別な空気摩擦が生じる時間 </summary>
    [Header("加速ギミック(気流)後のy軸空気摩擦のかかる時間")]
    [SerializeField] float m_moveTime = 1.0f;

    /// <summary>
    /// 上がる又は下がる高さ
    /// </summary>
    [Header("上がる又は下がる高さ")]
    [SerializeField] float amountMovement = 20.0f;

    [Header("動くときの緩急")]
    [SerializeField] Ease moveMethod = Ease.InOutFlash;

    KatsumataPlayerCameraAddforce cameraMove;

    [HideInInspector] public bool m_onDGMove = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cameraMove = other.gameObject.GetComponent<KatsumataPlayerCameraAddforce>();
            if (!cameraMove.cameraFixed)
            {
                cameraMove.ChangeAddSpeedCamera();
            }
            
            MovePoint(other.gameObject);
            m_onDGMove = true;
        }
    }

    void MovePoint(GameObject player)
    {
        if (!cameraMove.cameraFixed)
        {
            player.transform.DOLocalMoveY(amountMovement, m_moveTime)
            .SetRelative(true)
            .SetEase(moveMethod)
            .OnComplete(() => {
                cameraMove.ChangeBaseCamera();
                m_onDGMove = false;
            }); 
        }
        else
        {
            player.transform.DOLocalMoveY(amountMovement, m_moveTime)
            .SetRelative(true)
            .SetEase(moveMethod)
            .OnComplete(() => m_onDGMove = false);
        }
    }
}
