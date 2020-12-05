using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon_otsuki : MonoBehaviour
{
    [Header ("玉の発射位置")]
    public GameObject launchPort;
    [Header("玉の初期位置設定")]
    public GameObject bulletSet;
    [Header("玉の発射初期位置")]
    public Vector3 launchPort_Tr;
    // Start is called before the first frame update
    void Start()
    {
        launchPort_Tr = launchPort.transform.position;
        Instantiate(bulletSet, launchPort_Tr, Quaternion.identity);
    }
}
