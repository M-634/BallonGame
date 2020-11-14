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
    /// <summary>キャラクターが動く範囲にGroundLayerを設定してください。</summary>
    [SerializeField] LayerMask m_layerGround;
    /// <summary>ステージポイントをセットする。index[0]はスタートポイント</summary>
    [SerializeField] Transform[] m_stagePoints;
    [SerializeField] int m_currentStageIndex;//m_stagePointsのindex
   
    bool m_doMoveOK;
    NavMeshAgent m_agent;
    // Start is called before the first frame update
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        transform.position = m_stagePoints[0].position;
        m_currentStageIndex = 0;
        m_doMoveOK = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_doMoveOK) return;

        if (Input.GetKeyDown(KeyCode.RightArrow) && m_currentStageIndex + 1 < m_stagePoints.Length)
        {
            m_agent.destination = m_stagePoints[m_currentStageIndex + 1].position;
            m_currentStageIndex++;
            m_doMoveOK = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && m_currentStageIndex - 1 >= 0)
        {
            m_agent.destination = m_stagePoints[m_currentStageIndex - 1].position;
            m_currentStageIndex--;
            m_doMoveOK = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StagePoint"))
        {
            m_doMoveOK = true;
        }
    }
}
