using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBaseMain : MonoBehaviour
{
    [SerializeField] VolumeConfigUI m_volumeConfig;

    // Start is called before the first frame update
    void Start()
    {
        //設定画面のボリュームsliderの初期化
        m_volumeConfig.SetMaseterSliderEvent(vol => SoundManager.Instance.MasterVolume = vol);
        m_volumeConfig.SetBGMSliderEvent(vol => SoundManager.Instance.BGMVolume = vol);
        m_volumeConfig.SetGameSeSliderEvent(vol => SoundManager.Instance.GameSeVolume = vol);
        m_volumeConfig.SetEnvSliderEvent(vol => SoundManager.Instance.EnvironmentVolume = vol);
    }
}
