using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSceneManager : MonoBehaviour
{
    [SerializeField] string m_selectSceneBGM;

    void Start()
    {
        SoundManager.Instance.PlayBGMWithFadeIn(m_selectSceneBGM, 2f);    
    }
}
