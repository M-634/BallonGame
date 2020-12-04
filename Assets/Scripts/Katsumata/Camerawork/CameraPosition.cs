using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] GameObject trackingObj;
    [SerializeField] float adjustYposi = 10;
    [SerializeField] float adjustZposi = 10;
    Vector3 tracePosi;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tracePosi.y = trackingObj.transform.position.y + adjustYposi;
        tracePosi.z = trackingObj.transform.position.z + adjustZposi;
        transform.position = tracePosi;
    }
}
