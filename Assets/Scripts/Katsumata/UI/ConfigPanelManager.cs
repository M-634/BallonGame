using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigPanelManager : MonoBehaviour
{
    [SerializeField] GameObject configPanel;
    [SerializeField] GameObject endConfigButton;
    [SerializeField] GameObject camerafixedToggle;

    [SerializeField] Button endPauseButton;
    [SerializeField] Button beginConfigButton;

    public void BeginConfig()
    {
        configPanel.SetActive(true);
        endPauseButton.interactable = false;
        beginConfigButton.interactable = false;
    }

    public void EndConfig()
    {
        configPanel.SetActive(false);
        endPauseButton.interactable = true;
        beginConfigButton.interactable = true;
    }
}
