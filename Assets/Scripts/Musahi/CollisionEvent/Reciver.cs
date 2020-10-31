using UnityEngine;
using System.Collections;
using System;

public  class Reciver : MonoBehaviour,IDisposable
{
    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        UnSubscribe();
    }


    protected virtual void Subscribe()
    {
        Debug.Log("subscribe event....");
    }

    protected virtual void UnSubscribe()
    {
        Debug.Log("unSubscribe event....");
    }

    public void Dispose()
    {
        Debug.Log("yaAAAAAAAAAAAAAaa");
    }
}
