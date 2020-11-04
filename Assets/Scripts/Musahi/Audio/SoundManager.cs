using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// ゲーム全体のサウンドを管理するクラス
/// </summary>
public class SoundManager : SingletonMonoBehavior<SoundManager>
{

    [SerializeField,Header("MenuSE")]
    List<AudioClip> m_menuSeAuidoClipList = new List<AudioClip>();
    private AudioSource m_menuAudioSouce;

    [SerializeField,Header("EnviromentSE")]
    List<AudioClip> m_enviromentAudioClipList = new List<AudioClip>();
    private AudioSource m_enviromentAudioSource;

    [SerializeField,Header("VoiceSE")]
    List<AudioClip> m_voiceAudioClipList = new List<AudioClip>();
    private AudioSource m_voiceAudioSource;

    [SerializeField,Header("BGM")]
    List<AudioClip> m_bgmAuidoClipList = new List<AudioClip>();
    private List<AudioSource> m_bgmAudioSourceList = new List<AudioSource>();
    [SerializeField,Header("BGM数を設定してね")] int BGMAudiosorceNum;

    [SerializeField, Header("Audio Mixer")]
    AudioMixer m_audioMixer;
    [SerializeField] AudioMixerGroup m_bgmAMG, m_menuAMG, m_envAMG, m_voiceAMG;

    [SerializeField] AudioMixer m_effectAudioMixer;

    public bool IsPaused { get; private set; }

    private const string MasterVolumeParamName = "MasterVolume";
    private const string GameSeVolumeParamName = "GameSEVolume";
    private const string BGMVolumeParamName = "BGMVolume";
    private const string EnvVolumeParamName = "EnvironmentVolume";

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private AudioSource InitializeAudioSource(GameObject parentGameObject,bool isLoop = false,AudioMixerGroup amg = null)
    {
        var audioSource = parentGameObject.AddComponent<AudioSource>();

        audioSource.loop = isLoop;
        audioSource.playOnAwake = false;

        if (amg)
        {
            audioSource.outputAudioMixerGroup = amg;
        }

        return audioSource;
    }
}
