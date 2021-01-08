using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class KatsumataPlayerCameraAddforce : MonoBehaviour
{
    [Header("基本のカメラの位置、挙動")]
    [SerializeField] CinemachineVirtualCamera cmVcamBase;
    [Header("加速時のカメラの位置、挙動")]
    [SerializeField] CinemachineVirtualCamera cmVcamAddSpeed;

    CinemachineBrain cinemachineBrain;
    public float m_cameraChangeTime = 2.0f;

    /// <summary>
    /// カメラの固定化をするかどうか。初期値は固定しない状態。必ずUIのトグルを操作してオンオフ切り替えること
    /// </summary>
    [HideInInspector] public bool cameraFixed = false;

    private void Start()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
    }

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

    /// <summary>
    /// すぐにカメラをBaseCameraに切り替えたいとき使う
    /// </summary>
    public void ChangeBaseCamera()
    {
        cmVcamBase.Priority = 10;
        cmVcamAddSpeed.Priority = 5;
        Debug.Log("BaseCamera!");
    }
    /// <summary>
    /// 時間をかけてBaseCameraに切り替えたいとき使う
    /// </summary>
    /// <param name="timeCount"></param>
    public void ChangeBaseCamera(float timeCount)
    {
        StartCoroutine(CameraChangeCountTime(timeCount));
        Debug.Log("BaseCamera!");
    }

    IEnumerator CameraChangeCountTime(float time)
    {
        yield return new WaitForSeconds(time);
        cmVcamBase.Priority = 10;
        cmVcamAddSpeed.Priority = 5;
    }

    public void ChangeCameraFixed()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        if (cameraFixed)
        {
            cameraFixed = false;
            
            if (!cinemachineBrain.IsLive(cmVcamBase))
            {
                ChangeBaseCamera();
            }
            
        }
        else
        {
            cameraFixed = true;
        }
    }
}