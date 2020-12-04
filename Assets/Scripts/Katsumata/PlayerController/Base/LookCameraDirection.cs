using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCameraDirection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void FixedUpdate()
    {
        LookAtCameraTowards();
    }

    void LookAtCameraTowards()
    {
        Quaternion targetRotation = Quaternion.LookRotation(Camera.main.transform.forward);
        targetRotation.x = 0;
        targetRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
    }
}
