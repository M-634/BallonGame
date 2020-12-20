using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 加速ギミックに取り付ける。プレイヤーを指定した高さに上げたり下げたりする。
/// </summary>
public class PlayerOnStream : MonoBehaviour
{
    //Rigidbody m_rb;
    //[Header("追加の加速度")]
    ///// <summary>加速する係数 </summary>
    //[SerializeField] float m_addCoefficient = 50;

    //[Header("加速ギミック(気流)を抜けた直後のy軸空気摩擦")]
    ///// <summary>加速ギミックを抜けた後のy軸空気摩擦 </summary>
    //[SerializeField] float m_afterAccelDrag = 0.9f;

    [Header("加速ギミック(気流)後のy軸空気摩擦のかかる時間")]
    /// <summary>加速ギミックを抜けた後の特別な空気摩擦が生じる時間 </summary>
    [SerializeField] float m_moveTime = 1.0f;

    //[Header("加速ギミックを抜けた後のY軸の速さ")]
    ///// <summary>
    ///// 加速ギミックを抜けた後のY軸の速さ
    ///// m_afterAccelDragTimeの時間をかけてm_afterAccelDragの力をかけてこの速さにする
    ///// </summary>
    //[SerializeField] Vector3 m_afterAccelSpeed;

    [Header("上がる又は下がる高さ")]
    /// <summary>
    /// 上がる又は下がる高さ
    /// </summary>
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
            cameraMove.ChangeAddSpeedCamera();
            MovePoint(other.gameObject);
            m_onDGMove = true;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        //if (other.tag == "acceleration" && m_rb.velocity.magnitude < accelMaxSpeed)
        //{
        //    m_rb.AddForce(other.transform.forward * m_addCoefficient); //ローカル座標でのforward
        //}

    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.tag == "acceleration")
        //{
        //    //m_onHeightDrag = true;
        //    cameraMove.ChangeBaseCamera();

        //}
    }

    void MovePoint(GameObject player)
    {
        //var currentPosition = transform.position;
        Debug.Log("MovePoint!");
        player.transform.DOLocalMoveY(amountMovement, m_moveTime)
            .SetRelative(true)
            .SetEase(moveMethod)
            .OnComplete(() => cameraMove.ChangeBaseCamera())
            .OnComplete(() => m_onDGMove = false);
        //DOTween.To(
        //    () => currentPosition,
        //    (x) => transform.position = x,
        //    moveGoalPosi,
        //    m_moveTime)
        //    .SetEase(moveMethod)
        //    .OnComplete(() => cameraMove.ChangeBaseCamera())
        //    .OnComplete(() => m_onDGMove = false);
    }
}
