using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// ゲーム全体のサウンド(2D音源)を管理するクラス
/// </summary>
public class SoundManager : SingletonMonoBehavior<SoundManager>
{

    [SerializeField,Header("MenuSE")]
    List<AudioClip> m_menuSeAuidoClipList = new List<AudioClip>();
    private AudioSource m_menuSeAudioSouce;

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
    [SerializeField] AudioMixerGroup m_bgmAMG, m_menuSeAMG, m_envAMG, m_voiceAMG;

  
    public bool IsPaused { get; private set; }

    private const string MasterVolumeParamName = "MasterVolume";
    private const string GameSeVolumeParamName = "GameSEVolume";
    private const string BGMVolumeParamName = "BGMVolume";
    private const string EnvVolumeParamName = "EnvironmentVolume";

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        //必要な分のAudioSorceを予め用意する
        m_menuSeAudioSouce = InitializeAudioSource(this.gameObject, false, m_menuSeAMG);
        m_bgmAudioSourceList = InitializeAudioSources(this.gameObject, true, m_bgmAMG, BGMAudiosorceNum);
        m_enviromentAudioSource = InitializeAudioSource(this.gameObject, true, m_envAMG);
        m_voiceAudioSource = InitializeAudioSource(this.gameObject, false, m_voiceAMG);
    }

    //Audioの各種ボリュームのプロパティ
    public float MasterVolume
    {
        get { return m_audioMixer.GetVolumeByLinear(MasterVolumeParamName); }
        set { m_audioMixer.SetVolumeByLinear(MasterVolumeParamName, value); }
    }

    public float GameSeVolume 
    {
        get { return m_audioMixer.GetVolumeByLinear(GameSeVolumeParamName); }
        set { m_audioMixer.SetVolumeByLinear(GameSeVolumeParamName, value); }
    }

    public float BGMVolume
    {
        get { return m_audioMixer.GetVolumeByLinear(BGMVolumeParamName); }
        set { m_audioMixer.SetVolumeByLinear(BGMVolumeParamName, value); }
    }

    public float EnvironmentVolume
    {
        get { return m_audioMixer.GetVolumeByLinear(EnvVolumeParamName); }
        set { m_audioMixer.SetVolumeByLinear(EnvVolumeParamName, value); }
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

    private List<AudioSource> InitializeAudioSources(GameObject parentGameObject,bool isLoop = false,
        AudioMixerGroup amg = null,int count = 1)
    {
        List<AudioSource> audioSources = new List<AudioSource>();

        for (int i = 0; i < count; i++)
        {
            var audioSource = InitializeAudioSource(parentGameObject, isLoop, amg);
            audioSources.Add(audioSource);
        }
        return audioSources;
    }

    public void PlaySe(string clipName)
    {
        var audioClip = m_menuSeAuidoClipList.FirstOrDefault(clip => clip.name == clipName);

        if (audioClip == null)
        {
            Debug.LogWarning(clipName + "は見つかりません");
            return;
        }

        m_menuSeAudioSouce.Play(audioClip);
    }

    public void PlayEnviroment(string clipName)
    {

    }

    public void StopEnviromet(string clipName)
    {

    }

    public void PlayBGMWithFadeIn(string clipName, float fadeTime = 2f)
    {

    }

    public void StopBGMWithFadeOut(string clipName,float fadeTime = 2f)
    {

    }

    public void StopBGMWithFadeOut(float fadeTime = 2f)
    {

    }

    public void PlayVoice(string clipName,float delayTime = 0f)
    {

    }
}
