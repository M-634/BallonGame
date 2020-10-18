using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int m_getScore = 100;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreAndTimeManager.Instance.AddScore(m_getScore);
            gameObject.SetActive(false);
        }
    }
}
