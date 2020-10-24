using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Text playerSpeedText;
    [SerializeField] PlayerBaseMove playerBaseMove;
    [SerializeField] Text swipeDistanceText;
    [SerializeField] Button debugButton;
    [SerializeField] Button backButton;
    [SerializeField] GameObject debugPannel;
    public Slider swipeCoefficient;
    [SerializeField] Text swipeCoefficientText;
    public Slider forwardForce;
    [SerializeField] Text forwardForceText;
    public Slider forwardBrakeCoefficient;
    [SerializeField] Text forwardBrakeCoefficientText;
    [SerializeField] Text rotateForceText;
    [SerializeField] Text rotateBrakeCoefficientText;
    public Slider rotateBrakeCoefficient;
    [SerializeField] Text maxSpeedText;
    public Slider maxSpeed;


    private void Start()
    {
        playerBaseMove = player.GetComponent<PlayerBaseMove>();
    }
    // Update is called once per frame
    void Update()
    {
        playerSpeedText.text = "velocity,magnitude : " + playerBaseMove.Speed;
        swipeDistanceText.text = "swipeした距離 : " + playerBaseMove.swipeDistance_x;
        swipeCoefficientText.text = "horizontal : " + swipeCoefficient.value;
        forwardForceText.text = "forwardForce : " + forwardForce.value;
        forwardBrakeCoefficientText.text = "空気摩擦係数 : " + forwardBrakeCoefficient.value;
        rotateForceText.text = "rotateForce : " + playerBaseMove.debugRotateForce;
        rotateBrakeCoefficientText.text = "回転減衰係数 : " + rotateBrakeCoefficient.value;
        maxSpeedText.text = "最大速度 : " + maxSpeed.value;
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
        playerBaseMove.forwardBrekeCoefficient = forwardBrakeCoefficient.value;
    }
    /// <summary>これもデバッグ用、RotateBrakeCoefficientSliderのOnValueChangedで呼び出してる</summary>
    public void ChangeRotateBrakeCoefficient()
    {
        playerBaseMove.rotateBrekeCoefficient = rotateBrakeCoefficient.value;
    }
    /// <summary>これもデバッグ用、MaxSpeedSliderのOnValueChangedで呼び出してる</summary>
    public void ChangeMaxSpeed()
    {
        playerBaseMove.maxSpeed = maxSpeed.value;
    }
}