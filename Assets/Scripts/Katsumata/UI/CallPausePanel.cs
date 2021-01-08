using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallPausePanel : MonoBehaviour
{
    //[SerializeField] GameObject pauseText;
    //[SerializeField] GameObject returnButton;
    //[SerializeField] GameObject configButton;
    [SerializeField] Button beginPauseButton;
    [SerializeField] GameObject pausePanel;


    public void BeginPause()
    {
        Time.timeScale = 0.0f;
        pausePanel.SetActive(true);
        beginPauseButton.interactable = false;
        //pauseText.SetActive(true);
        //returnButton.SetActive(true);
        //configButton.SetActive(true);
    }

    public void EndPause()
    {
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
        beginPauseButton.interactable = true;
        //pauseText.SetActive(false);
        //returnButton.SetActive(false);
        //configButton.SetActive(false);
    }
}
