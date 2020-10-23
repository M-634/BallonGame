using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Text playerSpeedText;
    [SerializeField] PlayerBaseMove playerController;
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
        playerController = player.GetComponent<PlayerBaseMove>();
    }
    // Update is called once per frame
    void Update()
    {
        playerSpeedText.text = "velocity,magnitude : " + playerController.speed;
        swipeDistanceText.text = "swipeした距離 : " + playerController.swipeDistance_x;
        swipeCoefficientText.text = "horizontal : " + swipeCoefficient.value;
        forwardForceText.text = "forwardForce : " + forwardForce.value;
        forwardBrakeCoefficientText.text = "空気摩擦係数 : " + forwardBrakeCoefficient.value;
        rotateForceText.text = "rotateForce : " + playerController.debugRotateForce;
        rotateBrakeCoefficientText.text = "回転減衰係数 : " + rotateBrakeCoefficient.value;
        maxSpeedText.text = "最大速度 : " + maxSpeed.name;
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
}