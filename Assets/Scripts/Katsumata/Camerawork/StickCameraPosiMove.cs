using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StickCameraPosiMove : MonoBehaviour
{
    /// <summary>
    /// カメラの動きとなる元のスクリプト
    /// </summary>
    [SerializeField] GameObject variableJoystickobj;
    VariableJoystick variableJoystick;

    [SerializeField] GameObject freeLookCam;
    CinemachineFreeLook cinemafreeLook;

    Quaternion cameraRotation;
    /// <summary>
    /// カメラの動く範囲のカメラの角度制限
    /// </summary>
    [Header("カメラの動く円の角度範囲")]
    [SerializeField] float angle = 50;

    private void Start()
    {
        variableJoystick = variableJoystickobj.GetComponent<VariableJoystick>();
        cinemafreeLook = freeLookCam.GetComponent<CinemachineFreeLook>();
    }

    private void FixedUpdate()
    {
        LimitCameraMove();
    }

    void LimitCameraMove()
    {
        if (cinemafreeLook.m_XAxis.Value > angle && cinemafreeLook.m_XAxis.Value > 0)
        {
            cinemafreeLook.m_XAxis.Value = angle;
        }
        else if (cinemafreeLook.m_XAxis.Value < -angle && cinemafreeLook.m_XAxis.Value < 0)
        {
            cinemafreeLook.m_XAxis.Value = -angle;
        }
    }
}
