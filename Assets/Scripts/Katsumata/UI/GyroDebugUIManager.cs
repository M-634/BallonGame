using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroDebugUIManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] TestPlayerGyroRotater playerGyroRotater;

    [SerializeField] Text m_gyroText;
    [SerializeField] Text m_startGyroText;

    [SerializeField] Button gyroDebugButton;
    [SerializeField] Button backButton;
    [SerializeField] GameObject gyroDebugPannel;
    [SerializeField] Slider m_gyroCoefficient;


    // Start is called before the first frame update
    void Start()
    {
        playerGyroRotater = player.GetComponent<TestPlayerGyroRotater>();
    }

    // Update is called once per frame
    void Update()
    {
        m_gyroText.text = "Gyro { x : " + playerGyroRotater.m_gyro.x + " y : "
            + playerGyroRotater.m_gyro.y + " z : " + playerGyroRotater.m_gyro.z + " w : " + playerGyroRotater.m_gyro.w;
        m_startGyroText.text = "StartGyroRotation : " + playerGyroRotater.startGyroRotation;
    }

    public void OnGyroDebugButton()
    {
        gyroDebugButton.GetComponent<Button>().interactable = false;
        gyroDebugPannel.SetActive(true);
    }

    public void OnBackButton()
    {
        gyroDebugButton.GetComponent<Button>().interactable = true;
        gyroDebugPannel.SetActive(false);
    }

    public void ChangeGyroCoefficient()
    {
        playerGyroRotater.m_gyroCoefficient = m_gyroCoefficient.value;
    }
}
