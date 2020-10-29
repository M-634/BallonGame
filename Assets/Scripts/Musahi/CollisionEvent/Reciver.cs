using UnityEngine;
using System.Collections;

public  class Reciver : MonoBehaviour
{

    // Use this for initialization
    virtual protected void Start()
    {
        Subscribe();
    }

    protected virtual void Subscribe()
    {
        Debug.Log("subscribe event....");
    }

    protected virtual void UnSubscribe()
    {
        Debug.Log("unSubscribe event....");
    }
}
