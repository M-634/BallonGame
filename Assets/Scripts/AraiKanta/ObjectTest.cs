using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTest : MonoBehaviour
{
    public float x = 0f;   // X軸の値
    public float y = 0f;　// Y軸の値
    public float z = 0f;  // Z軸の値
    public float f = 0f;  // transform.forwardにかけているところ
    void Start()
    {
        
    }
    void Update()
    {
        this.transform.position += this.gameObject.transform.forward * f * Time.deltaTime;
        transform.Rotate(x * Time.deltaTime, y * Time.deltaTime, z * Time.deltaTime);
    }
}
