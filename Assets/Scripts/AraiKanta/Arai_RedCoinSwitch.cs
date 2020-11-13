using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arai_RedCoinSwitch : MonoBehaviour
{
    GameObject obj;
    [SerializeField] GameObject[] switchObj;
    void Start()
    {
        obj = GameObject.Find("RedCoindasuSwitch");
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
