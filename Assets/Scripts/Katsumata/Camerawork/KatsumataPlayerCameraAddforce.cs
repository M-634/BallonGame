using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class KatsumataPlayerCameraAddforce : MonoBehaviour
{
    [Header("基本のカメラの位置、挙動")]
    [SerializeField] CinemachineVirtualCamera cmVcamBase;
    [Header("加速時のカメラの位置、挙動")]
    [SerializeField] CinemachineVirtualCamera cmVcamAddSpeed;
    [Header("加速した時のカメラから通常時のカメラに切り替わるまでの時間")]
    public float m_cameraChangeTime = 2.0f;
    Coroutine cameraChangeCountCoroutine;
    /// <summary>
    /// カメラの固定化をするかどうか。初期値は固定しない状態。必ずUIのトグルを操作してオンオフ切り替えること
    /// </summary>
    [HideInInspector] public bool cameraFixed = false;


    /// <summary>
    /// プレイヤーが加速オブジェの効果を得た時のカメラ。
    /// カメラのDampingを上げて少し遅れてついていく演出を加える
    /// </summary>
    public void ChangeAddSpeedCamera()
    {
        cmVcamBase.Priority = 5;
        cmVcamAddSpeed.Priority = 10;
        //Debug.Log("AddSpeedCamera!");
    }

    /// <summary>
    /// すぐにカメラをBaseCameraに切り替えたいとき使う
    /// </summary>
    public void ChangeBaseCamera()
    {
        cmVcamBase.Priority = 10;
        cmVcamAddSpeed.Priority = 5;
        //Debug.Log("BaseCamera!");
    }
    /// <summary>
    /// 時間をかけてBaseCameraに切り替えたいとき使う
    /// </summary>
    /// <param name="timeCount"></param>
    public void ChangeBaseCamera(float timeCount)
    {
        if (cameraChangeCountCoroutine != null)
        {
            StopCoroutine(cameraChangeCountCoroutine);
        }
        cameraChangeCountCoroutine = StartCoroutine(CameraChangeCountTime(timeCount));
        //Debug.Log("BaseCamera!");
    }

    IEnumerator CameraChangeCountTime(float time)
    {
        yield return new WaitForSeconds(time);
        cmVcamBase.Priority = 10;
        cmVcamAddSpeed.Priority = 5;
    }

    public void ChangeCameraFixed()
    {
        if (cameraFixed) //カメラ固定モードから固定しないモードに切り替え
        {
            cameraFixed = false;
        }
        else //カメラ固定しないモードから固定モードに切り替える。その際すぐにCMvcamBaseに切り替える
        {
            cameraFixed = true;
            if (cameraChangeCountCoroutine != null)
            {
                StopCoroutine(cameraChangeCountCoroutine);
            }
            ChangeBaseCamera();
        }
    }
}