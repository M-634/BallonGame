using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Player_Musashi : MonoBehaviour
{
    [SerializeField] GameState m_gameState;
    public float m_speed = 1f;

    private void Awake()
    {
        m_gameState = FindObjectOfType<GameState>();
        if (m_gameState == null)
        {
            Debug.LogError("TimeSchedulerコンポーネントをアタッチされたゲームオブジェクトが存在しません");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_gameState.InGame) return;

        transform.position += new Vector3(0, 0, m_speed / 90f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IEventCollision>(out var eventCollision))
        {
            eventCollision.CollisionEvent();
        }
    }
}
