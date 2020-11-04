using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBaseMain : MonoBehaviour
{
    [SerializeField] VolumeConfigUI m_volumeConfig;

    // Start is called before the first frame update
    void Start()
    {
        m_volumeConfig.SetMaseterSliderEvent(v => SoundManager.Instance.MasterVolume = v);
    }
}
