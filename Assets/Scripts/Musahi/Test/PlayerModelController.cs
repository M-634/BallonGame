using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// キャラクターのアニメーションと
/// NaviMeshを使った動きを管理するクラス
/// セレクトシーンのみ使用する
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerModelController : MonoBehaviour
{
    /// <summary>子オブジェクトのキャラクターモデルにアタッチされているAnimatorをアサインする</summary>
    [SerializeField] Animator m_animator;
    [SerializeField] LayerMask m_layerGround;

    bool m_doMoveOK;
    NavMeshAgent m_agent;
    Vector3 m_targetPos;
    // Start is called before the first frame update
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        //マウスをクリックした場所へ移動する
        if (Input.GetMouseButtonDown(0) && m_doMoveOK)
        {
            GoHitPoint();
        }

        //着いたらアニメーションを止める
        if (ArriveDestination())
        {
            m_animator.SetFloat("Speed", 0f);
            m_doMoveOK = true;
        }
    }

    private void GoHitPoint()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, m_layerGround))
        {
            transform.LookAt(hit.point);
            m_agent.destination = hit.point;
            m_targetPos = hit.point;
            m_animator.SetFloat("Speed", m_agent.speed);
            m_doMoveOK = false;
        }
    }

    private bool ArriveDestination()
    {
        Vector3 temp = m_targetPos;
        temp.y = 0;
        m_targetPos = temp;

        Vector3 playerPos = transform.position;
        playerPos.y = 0;

        return Vector3.Distance(m_targetPos, playerPos) < 0.1f;
    }
}
