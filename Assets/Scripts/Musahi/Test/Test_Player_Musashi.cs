using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Player_Musashi : MonoBehaviour
{
    TimerInStage m_gameState;
    public float m_speed = 1f;

    EventSystemInGameScene m_eventSystemInGameScene;
    private void Awake()
    {
        //ここもっといいやり方ないかなー|дﾟ)
        m_gameState = FindObjectOfType<TimerInStage>();
        if (m_gameState == null)
        {
            Debug.LogError("TimeSchedulerコンポーネントをアタッチされたゲームオブジェクトが存在しません");
        }
        m_eventSystemInGameScene = FindObjectOfType<EventSystemInGameScene>();
       if (m_eventSystemInGameScene == null)
        {
            Debug.LogError("EventSystemInGameSceneコンポーネントがアタッチされたゲームオブジェクトが存在しません");
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
            eventCollision.CollisionEvent(m_eventSystemInGameScene);
        }
    }
}
