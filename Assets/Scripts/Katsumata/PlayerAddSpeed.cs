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
    [SerializeField] int accelWaitTime = 2500;
    bool onWaitAccel = false;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "acceleration" && !onWaitAccel)
        {
            m_rb.AddForce(this.transform.forward * addCoefficient);
            onWaitAccel = true;
            var task = DelayAsync().ContinueWith(t => onWaitAccel = false);
            
        }
    }

    async Task DelayAsync()
    {
        await Task.Delay(accelWaitTime);
        Debug.Log("加速待ち");
        
        //return onWaitAccel = false;
    }

}
