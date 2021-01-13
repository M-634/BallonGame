using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// ゲーム全体のサウンドを管理するクラス
/// </summary>
public class SoundManager : SingletonMonoBehavior<SoundManager>
{
    //メンバー変数
    #region
    [SerializeField,Header("MenuSE")]
    List<AudioClip> m_menuSeAuidoClipList = new List<AudioClip>();
    private AudioSource m_menuSeAudioSouce;

    [SerializeField,Header("GameSE")]
    List<AudioClip> m_gameSeAudioClipList = new List<AudioClip>();
    AudioSource m_gameSeAudioSource;

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
    [SerializeField] AudioMixerGroup m_bgmAMG, m_menuSeAMG,m_gameSeAMG, m_envAMG, m_voiceAMG;

  
    public bool IsPaused { get; private set; }

    private const string MasterVolumeParamName = "MasterVolume";
    private const string GameSeVolumeParamName = "GameSEVolume";
    private const string BGMVolumeParamName = "BGMVolume";
    private const string EnvVolumeParamName = "EnvironmentVolume";
    #endregion

    //Audioの各種ボリュームのプロパティ
    #region
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
    #endregion

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        //必要な分のAudioSorceを予め用意する
        m_menuSeAudioSouce = InitializeAudioSource(this.gameObject, false, m_menuSeAMG);
        m_gameSeAudioSource = InitializeAudioSource(this.gameObject, false, m_gameSeAMG);
        m_bgmAudioSourceList = InitializeAudioSources(this.gameObject, true, m_bgmAMG, BGMAudiosorceNum);
        m_enviromentAudioSource = InitializeAudioSource(this.gameObject, true, m_envAMG);
        m_voiceAudioSource = InitializeAudioSource(this.gameObject, false, m_voiceAMG);
    }

    //AudioSoureceの初期化 
    #region
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
    #endregion

    //メソッド
    #region 
    public void PlayMenuSe(string clipName)
    {
        var audioClip = m_menuSeAuidoClipList.FirstOrDefault(clip => clip.name == clipName);

        if (audioClip == null)
        {
            Debug.LogWarning(clipName + "は見つかりません");
            return;
        }

        m_menuSeAudioSouce.Play(audioClip);
    }

    /// <summary>
    /// ゲームシーンで繰り返しなる音はピッチをランダムに、
    /// 特殊演出などの音源は少しボリュームを上げる
    /// </summary>
    /// <param name="clipName"></param>
    /// <param name="pitchRandom"></param>
    /// <param name="range"></param>
    public void PlayGameSe(string clipName,bool pitchRandom = true,float range = 0.5f)
    {
        var audioClip = m_gameSeAudioClipList.FirstOrDefault(clip => clip.name == clipName);

        if (audioClip == null)
        {
            Debug.LogWarning(clipName + "は見つかりません");
            return;
        }

        if (pitchRandom)
        {
            range = Mathf.Clamp(range, 0.1f, 1f);
            m_gameSeAudioSource.pitch = Random.Range(1f - range, 1f + range);
        }
        else
        {
            m_gameSeAudioSource.pitch = 1;
        }
        m_gameSeAudioSource.Play(audioClip); 
    }

    public void PlayEnviroment(string clipName)
    {
        var audioClip = m_enviromentAudioClipList.FirstOrDefault(clip => clip.name == clipName);

        if (audioClip == null)
        {
            Debug.LogWarning(clipName + "は見つかりません");
            return;
        }

        m_enviromentAudioSource.Play(audioClip);
    }

    public void StopEnviromet()
    {
        if (m_enviromentAudioSource.isPlaying)
        {
            m_enviromentAudioSource.Stop();
        }
    }

    public void PlayBGMWithFadeIn(string clipName, float fadeTime = 2f)
    {
        var audioClip = m_bgmAuidoClipList.FirstOrDefault(clip => clip.name == clipName);

        if (audioClip == null)
        {
            Debug.LogWarning(clipName + "は見つかりません");
            return;
        }

        if (m_bgmAudioSourceList.Any(source => source.clip == audioClip))
        {
            Debug.Log(clipName + "はすでに再生されています");
            return;
        }

        StopBGMWithFadeOut(fadeTime);//再生中のBGMをFadeOut

        var audioSource = m_bgmAudioSourceList.FirstOrDefault(bgm => bgm.isPlaying == false);

        if (audioSource)
        {
            audioSource.PlayWithFadeIn(audioClip, fadeTime);
        }
    }

    public void StopBGMWithFadeOut(string clipName,float fadeTime = 2f)
    {
        var audioSource = m_bgmAudioSourceList.FirstOrDefault(bgm => bgm.clip.name == clipName);

        if (audioSource == null || audioSource.isPlaying == false)
        {
            Debug.Log(clipName + "は再生されていません！！");
            return;
        }

        audioSource.StopWithFadeOut(fadeTime);
    }

    public void StopBGMWithFadeOut(float fadeTime = 2f)
    {
        var audioSorceList = m_bgmAudioSourceList.Where(bgm => bgm.isPlaying).ToList();
        foreach (var audioSource in audioSorceList)
        {
            audioSource.StopWithFadeOut(fadeTime);
        }
    }

    public void PlayVoice(string clipName,float delayTime = 0f)
    {
        var audioClip = m_voiceAudioClipList.FirstOrDefault(clip => clip.name == clipName);

        if (audioClip == null)
        {
            Debug.LogWarning(clipName + "は見つかりません");
            return;
        }

        m_voiceAudioSource.clip = audioClip;
        m_voiceAudioSource.PlayDelayed(delayTime);

        m_voiceAudioSource.PlayScheduled(AudioSettings.dspTime + delayTime);
    }
    #endregion
}
