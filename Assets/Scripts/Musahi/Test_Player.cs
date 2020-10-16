using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Player : MonoBehaviour
{
    public float m_speed = 1f;
    // Update is called once per frame
    void Update()
    {
        if (ScoreAndTimeManager.Instance.InGame)
        {
            transform.position += new Vector3(0, 0, m_speed / 90f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Obstacle")
        {
            ScoreAndTimeManager.Instance.GameOver();
        }
    }
}
