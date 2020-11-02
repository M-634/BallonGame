using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Text playerSpeedText;
    [SerializeField] PlayerBaseMove playerBaseMove;
    [SerializeField] Text XaxisSwipeDistanceText;
    [SerializeField] Text YaxisSwipeDistanceText;
    [SerializeField] Text swipe;

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
    }
    // Update is called once per frame
    void Update()
    {
        playerSpeedText.text = "velocity,magnitude : " + playerBaseMove.Speed;
        XaxisSwipeDistanceText.text = "横方向にswipeした距離 : " + playerBaseMove.swipeDistance_x;
        YaxisSwipeDistanceText.text = "縦方向にswipeした距離 : " + playerBaseMove.swipeDistance_y;
        swipe.text = "swipe : " + playerBaseMove.swipe;
        swipeCoefficientText.text = "horizontal : " + swipeCoefficient.value;
        forwardForceText.text = "forwardForce : " + forwardForce.value;
        forwardBrakeCoefficientText.text = "空気摩擦係数 : " + forwardBrakeCoefficient.value;
        //rotateForceText.text = "rotateForce : " + playerBaseMove.RotateSpeed;
        rotateBrakeCoefficientText.text = "回転減衰係数 : " + rotateBrakeCoefficient.value;
        maxSpeedText.text = "最大速度 : " + maxSpeedSlider.value;
        addAirBrakeText.text = "下スワイプしたときのブレーキの係数 : " + addAirBrakeSlider.value;
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
        playerBaseMove.horizontalSpeed = swipeCoefficient.value;
    }

    /// <summary>これもデバッグ用、FowrardForceSliderのOnValueChangedで呼び出してる</summary>
    public void ChangeForwardForce()
    {
        playerBaseMove.forwardForce = forwardForce.value;
    }

    /// <summary>これもデバッグ用、airBreakCoefficientSliderのOnValueChangedで呼び出してる</summary>
    public void ChangeForwardBrakeCoefficient()
    {
        playerBaseMove.airBrekeCoefficient = forwardBrakeCoefficient.value;
    }
    /// <summary>これもデバッグ用、RotateBrakeCoefficientSliderのOnValueChangedで呼び出してる</summary>
    public void ChangeRotateBrakeCoefficient()
    {
        playerBaseMove.rotateBrekeCoefficient = rotateBrakeCoefficient.value;
    }
    /// <summary>これもデバッグ用、MaxSpeedSliderのOnValueChangedで呼び出してる</summary>
    public void ChangeMaxSpeed()
    {
        playerBaseMove.maxSpeed = maxSpeedSlider.value;
    }
    /// <summary>これもデバッグ用、MaxSpeedSliderのOnValueChangedで呼び出してる</summary>
    public void ChangeAddAirBrake()
    {
        playerBaseMove.addAirBrake = addAirBrakeSlider.value;
    }
}