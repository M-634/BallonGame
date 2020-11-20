using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class KatsumataPlayerCameraMove : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cmVcamBase;
    [SerializeField] CinemachineVirtualCamera cmVcamAddSpeed;

    /// <summary>
    /// プレイヤーが加速オブジェの効果を得た時のカメラ。
    /// カメラのDampingを上げて少し遅れてついていく演出を加える
    /// </summary>
    public void ChangeAddSpeedCamera()
    {
        cmVcamBase.Priority = 5;
        cmVcamAddSpeed.Priority = 10;
        Debug.Log("AddSpeedCamera!");
    }

    public void ChangeBaseCamera()
    {
        cmVcamBase.Priority = 10;
        cmVcamAddSpeed.Priority = 5;
        Debug.Log("BaseCamera!");
    }
}