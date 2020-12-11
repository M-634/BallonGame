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
        tracePosi = trackingObj.transform.position;
        tracePosi.y = trackingObj.transform.position.y + adjustYposi;
        tracePosi.z = trackingObj.transform.position.z + adjustZposi;
        transform.position = tracePosi;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tracePosi.y = trackingObj.transform.position.y + adjustYposi;
        tracePosi.z = trackingObj.transform.position.z + adjustZposi;
        transform.position = tracePosi;
    }
}
