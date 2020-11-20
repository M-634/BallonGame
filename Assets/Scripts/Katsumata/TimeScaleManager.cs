using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ザ・ワールド！！時よとまれ！！とかやるクラス
/// </summary>
public class TimeScaleManager : MonoBehaviour
{
    public void StopTime()
    {
        Time.timeScale = 0;
    }
    public void StartTime()
    {
        Time.timeScale = 1;
    }
}
