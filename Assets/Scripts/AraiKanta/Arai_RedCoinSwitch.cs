using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// レッドコイン出現に必要なswitchにつけるスクリプト
/// </summary>
public class Arai_RedCoinSwitch : MonoBehaviour
{
    GameObject obj;
    [Header("RedCoinのオブジェクト")]
    [SerializeField] GameObject[] switchObj;
    void Start()
    {
        obj = GameObject.Find("RedCoinPopUpSwitch");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (var item in switchObj)
            {
                item.SetActive(true);                
                Debug.Log("coin出現");
            }
            obj.SetActive(false);
            Debug.Log("switch消えた");
        }
    }
}
