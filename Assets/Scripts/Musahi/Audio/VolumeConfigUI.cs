﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 音量設定を管理するクラス
/// </summary>
public class VolumeConfigUI : MonoBehaviour
{
    public bool m_isDubug;

    [SerializeField] CanvasGroup m_canvasGroup;
    [SerializeField, Header("各種Sliderをセットする")]
    Slider m_masterSlider, m_bgmSlider, m_seSlider, m_envSlider;


    // Start is called before the first frame update
    void Start()
    {
        if (SoundManager.Instance)
        {
            //設定画面のボリュームsliderの初期化
            SetMaseterSliderEvent(vol => SoundManager.Instance.MasterVolume = vol);
            SetBGMSliderEvent(vol => SoundManager.Instance.BGMVolume = vol);
            SetGameSeSliderEvent(vol => SoundManager.Instance.GameSeVolume = vol);
            SetEnvSliderEvent(vol => SoundManager.Instance.EnvironmentVolume = vol);
        }

        if (m_isDubug)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public void Show()
    {
        m_canvasGroup.Show();
    }

    public void Hide()
    {
        m_canvasGroup.Hide();
    }

    public void SetMasterVolume(float masterVolume)
    {
        m_masterSlider.value = masterVolume;
    }

    public void SetBGMVolume(float bgmVolume)
    {
        m_bgmSlider.value = bgmVolume;
    }

    public void SetSeVolume(float seVolume)
    {
        m_seSlider.value = seVolume;
    }

    public void SetEnvVolume(float envVolume)
    {
        m_envSlider.value = envVolume;
    }

    public void SetMaseterSliderEvent(UnityAction<float> sliderCallback)
    {
        m_masterSlider.SetValueChangedEvent(sliderCallback);
    }

    public void SetBGMSliderEvent(UnityAction<float> sliderCallback)
    {
        m_bgmSlider.SetValueChangedEvent(sliderCallback);
    }

    public void SetGameSeSliderEvent(UnityAction<float> sliderCallback)
    {
        m_seSlider.SetValueChangedEvent(sliderCallback);
    }

    public void SetEnvSliderEvent(UnityAction<float> sliderCallback)
    {
        m_envSlider.SetValueChangedEvent(sliderCallback);
    }
}