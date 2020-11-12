using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

/// <summary>
/// マリオカートのキノコダッシュを想定している
/// </summary>
public class PlayerAddSpeed : MonoBehaviour
{
    Rigidbody m_rb;
    /// <summary>加速する係数 </summary>
    [SerializeField] float addCoefficient = 1500;
    /// <summary>次に加速が始まるまでの待機時間 </summary>
    [SerializeField] float accelWaitTime = 2.5f;
    bool onAccelable = true;
    float waitTime = 0;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        if (!onAccelable)
        {
            waitTime += Time.deltaTime;
            onAccelable = waitTime > accelWaitTime ? true : false;
        }
        else
        {
            waitTime = 0;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "acceleration" && onAccelable)
        {
            m_rb.AddForce(transform.forward * addCoefficient);
            onAccelable = false;
        }
    }


}
