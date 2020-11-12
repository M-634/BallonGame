using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAddSpeed : MonoBehaviour
{
    [SerializeField] float addCoefficient = 300;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "acceleration")
        {
            PlayerBaseMove.m_rb.AddForce(this.transform.forward* addCoefficient);
        }
    }
}
