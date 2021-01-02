using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedXAxisPosition : MonoBehaviour
{
    Vector3 firstPosi;
    // Start is called before the first frame update
    void Start()
    {
        firstPosi = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newposi = transform.position;
        newposi.x = firstPosi.x;
        transform.position = newposi;
    }
}
