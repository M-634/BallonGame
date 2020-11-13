using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeyaKogarashi : MonoBehaviour
{
    [Header("葉っぱの画像 -キャンバスを用意してほしい-")]
    [SerializeField] GameObject leaf;

    //タイムリミット
    [Header("消えるまでの時間")]
    [SerializeField] float timeLimit = 5.0f;
    private void Start()
    {
        leaf.SetActive(false);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            leaf.SetActive(true);
            StartCoroutine("Timer");
        }
    }
    IEnumerator Timer()
    {
        while (timeLimit >= 0)
        {
            Debug.Log(timeLimit);
            yield return new WaitForSeconds(1.0f);
            timeLimit -= 1.0f;
            if (timeLimit <= 0)
            {
                leaf.SetActive(false);
            }
            yield return null;
        }
    }

}
