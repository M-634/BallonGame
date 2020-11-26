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
    [SerializeField] GameObject m_centerOfAnchor;
    Rigidbody m_anchorRb;
    Rigidbody m_parentRb;
    [SerializeField] float moveRatio = 0.99f;
    [SerializeField] float slantXRange = 1.0f;
    [SerializeField] float slantYRange = 0.2f;
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
        Debug.Log("AnchorとCenterOfAnchorの距離：" + Vector3.Distance(m_anchor.transform.position, m_centerOfAnchor.transform.position));
    }

    void SetSlant()
    {
        Quaternion targetRotation = Quaternion.LookRotation(m_anchor.transform.position - transform.position);
        //Quaternion rotate;
        //rotate = Quaternion.AngleAxis(targetRotation.eulerAngles.x, Vector3.right);
        //rotate *= Quaternion.AngleAxis(targetRotation.eulerAngles.y, Vector3.up);
        //rotate *= Quaternion.AngleAxis(targetRotation.eulerAngles.z, Vector3.forward);
        //targetRotation.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
    }

    void SetAnchorPosi()
    {
        m_anchorRb.velocity = m_parentRb.velocity * moveRatio; //アンカーはやや遅れてついてくるようにする
        AdjustAnchorPosi();
    }

    void AdjustAnchorPosi()
    {
        Vector3 m_anchorPosi = m_anchor.transform.position;
        Vector3 m_centerAnchorPosi = m_centerOfAnchor.transform.position;
        m_anchor.transform.position =
            new Vector3(Mathf.Clamp(m_anchorPosi.x, m_centerAnchorPosi.x - slantXRange, m_centerAnchorPosi.x + slantXRange),
            Mathf.Clamp(m_anchorPosi.y, m_centerAnchorPosi.y - slantYRange, m_centerAnchorPosi.y + slantYRange),
            Mathf.Clamp(m_anchorPosi.z, m_centerAnchorPosi.z - slantZRange, m_centerAnchorPosi.z + slantZRange));
    }
}
