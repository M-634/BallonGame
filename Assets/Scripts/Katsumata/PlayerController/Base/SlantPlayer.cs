using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの揺れを処理するクラス
/// </summary>
public class SlantPlayer : MonoBehaviour
{
    /// <summary>バルーンのアンカー </summary>
    [SerializeField] GameObject m_anchor;
    /// <summary>バルーンのアンカーの中心点 </summary>
    [SerializeField] GameObject m_centerOfAnchor;
    Rigidbody m_anchorRb;
    Rigidbody m_parentRb;
    /// <summary>アンカーとバルーンの速度の比 </summary>
    [Header("アンカーとバルーンの速度の比")]
    [SerializeField] float moveRatio = 0.99f;
    /// <summary>アンカーがcenterOfAnchorからどれ程の領域を動けるかを制限する値 </summary>
    [Header("アンカーの中心からアンカーが動ける範囲")]
    [SerializeField] float slantXRange = 1.0f;
    [SerializeField] float slantYRange = 0.6f;
    [SerializeField] float slantZRange = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_parentRb = transform.parent.GetComponent<TouchForcePlayerMove>().m_rb;
        m_anchorRb = m_anchor.GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        SetSlant();
        SetAnchorPosi();
        //Debug.Log("AnchorとCenterOfAnchorの距離：" + Vector3.Distance(m_anchor.transform.position, m_centerOfAnchor.transform.position));
        
    }

    /// <summary>
    /// 揺れを実装する機能の本体
    /// </summary>
    void SetSlant()
    {
        Quaternion targetRotation = Quaternion.LookRotation(m_anchor.transform.position - transform.position,Vector3.back);

        Debug.Log("targetRotationのQuaternion x:" + targetRotation.x + " y:" + targetRotation.y + " z:" + targetRotation.z + " w:" + targetRotation.w);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
    }

    
    /// <summary>
    /// アンカー本体の動き
    /// </summary>
    void SetAnchorPosi()
    {
        m_anchorRb.velocity = m_parentRb.velocity * moveRatio; //アンカーはやや遅れてついてくるようにする
        AdjustAnchorPosi();
    }

    /// <summary>
    /// アンカーが、アンカーの中心からの移動範囲を制限する
    /// </summary>
    void AdjustAnchorPosi()
    {
        Vector3 anchorPosi = m_anchor.transform.position;
        Vector3 centerAnchorPosi = m_centerOfAnchor.transform.position;
        m_anchor.transform.position =
            new Vector3(Mathf.Clamp(anchorPosi.x, centerAnchorPosi.x - slantXRange, centerAnchorPosi.x + slantXRange),
            Mathf.Clamp(anchorPosi.y, centerAnchorPosi.y - slantYRange, centerAnchorPosi.y + slantYRange),
            Mathf.Clamp(anchorPosi.z, centerAnchorPosi.z - slantZRange, centerAnchorPosi.z + slantZRange));
    }
}
