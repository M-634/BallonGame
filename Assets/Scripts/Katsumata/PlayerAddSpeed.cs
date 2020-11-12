using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マリオカートのキノコダッシュを想定している
/// </summary>
public class PlayerAddSpeed : MonoBehaviour
{
    Rigidbody m_rb;
    /// <summary>加速する係数 </summary>
    [SerializeField] float addCoefficient = 300;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "acceleration")
        {
            m_rb.AddForce(this.transform.forward * addCoefficient);
        }
    }

}
