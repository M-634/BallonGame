using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Text playerSpeedText;
    [SerializeField] PlayerController playerController;
    [SerializeField] Text swipeDistanceText;
    [SerializeField] Button debugButton;
    [SerializeField] Button backButton;
    [SerializeField] GameObject debugPannel;
    public Slider swipeCoefficient;
    [SerializeField] Text swipeCoefficientText;
    public Slider forwardForce;
    [SerializeField] Text forwardForceText;
    public Slider brakeCoefficient;
    [SerializeField] Text brakeCoefficientText;

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        playerSpeedText.text = "velocity,magnitude : " + playerController.speed;
        swipeDistanceText.text = "swipeした距離 : " + playerController.swipeDistance_x;
        swipeCoefficientText.text = "horizontal : " + swipeCoefficient.value;
        forwardForceText.text = "forwardForce : " + forwardForce.value;
        brakeCoefficientText.text = "空気摩擦係数 : " + brakeCoefficient.value;
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
