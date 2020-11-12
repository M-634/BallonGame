using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Text playerSpeedText;
    [SerializeField] PlayerBaseMove playerBaseMove;
    [SerializeField] TestPlayerGyroRotater playerGyroRotater;
    [SerializeField] Text XaxisSwipeDistanceText;
    [SerializeField] Text YaxisSwipeDistanceText;
    [SerializeField] Text swipe;
    [SerializeField] Text gyroText;

    [SerializeField] Button debugButton;
    [SerializeField] Button backButton;
    [SerializeField] GameObject debugPannel;
    public Slider swipeCoefficient;
    [SerializeField] Text swipeCoefficientText;
    public Slider forwardForce;
    [SerializeField] Text forwardForceText;
    public Slider forwardBrakeCoefficient;
    [SerializeField] Text forwardBrakeCoefficientText;
    //[SerializeField] Text rotateForceText;
    [SerializeField] Text rotateBrakeCoefficientText;
    public Slider rotateBrakeCoefficient;
    [SerializeField] Text maxSpeedText;
    public Slider maxSpeedSlider;
    [SerializeField] Text addAirBrakeText;
    public Slider addAirBrakeSlider;
    


    private void Start()
    {
        playerBaseMove = player.GetComponent<PlayerBaseMove>();
        playerGyroRotater = player.GetComponent<TestPlayerGyroRotater>();
    }
    // Update is called once per frame
    void Update()
    {
        playerSpeedText.text = "velocity,magnitude : " + playerBaseMove.mp_Speed;
        XaxisSwipeDistanceText.text = "横方向にswipeした距離 : " + playerBaseMove.m_swipeDistance_x;
        YaxisSwipeDistanceText.text = "縦方向にswipeした距離 : " + playerBaseMove.m_swipeDistance_y;
        swipe.text = "swipe : " + playerBaseMove.m_onSwipe;
        swipeCoefficientText.text = "horizontal : " + swipeCoefficient.value;
        forwardForceText.text = "forwardForce : " + forwardForce.value;
        forwardBrakeCoefficientText.text = "空気摩擦係数 : " + forwardBrakeCoefficient.value;
        //rotateForceText.text = "rotateForce : " + playerBaseMove.RotateSpeed;
        rotateBrakeCoefficientText.text = "回転減衰係数 : " + rotateBrakeCoefficient.value;
        maxSpeedText.text = "最大速度 : " + maxSpeedSlider.value;
        addAirBrakeText.text = "下スワイプしたときのブレーキの係数 : " + addAirBrakeSlider.value;
        gyroText.text = "Gyro { x : " + playerGyroRotater.m_gyro.x + " y : "
            + playerGyroRotater.m_gyro.y + " z : " + playerGyroRotater.m_gyro.z + " w : " + playerGyroRotater.m_gyro.w;
    }

    public void OnDebugButton()
    {
        debugButton.GetComponent<Button>().interactable = false;
        debugPannel.SetActive(true);
    }

    public void OnBackButton()
    {
        debugButton.GetComponent<Button>().interactable = true;
        debugPannel.SetActive(false);
    }

    /// <summary>デバッグ用、SwipeCoefficientSliderのOnValueChangedで呼び出してる</summary>
    public void ChangeSwipeCoefficient()
    {
        playerBaseMove.m_horizontalSpeed = swipeCoefficient.value;
    }

    /// <summary>これもデバッグ用、FowrardForceSliderのOnValueChangedで呼び出してる</summary>
    public void ChangeForwardForce()
    {
        playerBaseMove.m_forwardForce = forwardForce.value;
    }

    /// <summary>これもデバッグ用、airBreakCoefficientSliderのOnValueChangedで呼び出してる</summary>
    public void ChangeForwardBrakeCoefficient()
    {
        playerBaseMove.m_airBrekeCoefficient = forwardBrakeCoefficient.value;
    }
    /// <summary>これもデバッグ用、RotateBrakeCoefficientSliderのOnValueChangedで呼び出してる</summary>
    public void ChangeRotateBrakeCoefficient()
    {
        playerBaseMove.m_rotateBrekeCoefficient = rotateBrakeCoefficient.value;
    }
    /// <summary>これもデバッグ用、MaxSpeedSliderのOnValueChangedで呼び出してる</summary>
    public void ChangeMaxSpeed()
    {
        playerBaseMove.m_maxSpeed = maxSpeedSlider.value;
    }
    /// <summary>これもデバッグ用、MaxSpeedSliderのOnValueChangedで呼び出してる</summary>
    public void ChangeAddAirBrake()
    {
        playerBaseMove.m_addAirBrake = addAirBrakeSlider.value;
    }
}