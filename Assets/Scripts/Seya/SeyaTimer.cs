using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeyaTimer : MonoBehaviour
{
    [SerializeField] float limitTime = 1.0f;
    private void Start()
    {
        StartCoroutine("Timer");
    }
    IEnumerator Timer()
    {
        while (limitTime >= 0)
        {
            Debug.Log(limitTime);
            yield return new WaitForSeconds(1.0f);
            limitTime -= 1.0f;
        }
    }
}
