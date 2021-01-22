using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 3D音源プレイヤー
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class GameSePlayer : MonoBehaviour
{
    [SerializeField] List<AudioClip> m_audioClipList = new List<AudioClip>();
    AudioSource m_audioSource;

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_audioSource.playOnAwake = false;
        m_audioSource.spatialBlend = 0.7f;
    }

    public void PlayFirstAudioClip(float range = 0.5f)
    {
        if (m_audioClipList.Count > 0)
        {
            //m_audioSource.pitch = 1f;
            m_audioSource.pitch = Random.Range(1f - range, 1f + range);
            m_audioSource.Play(m_audioClipList[0]);
        }
    }

    public void PlaySe(string clipName)
    {
        var audioClip = m_audioClipList.FirstOrDefault(clip => clip.name == clipName);

        if (audioClip)
        {
            m_audioSource.pitch = 1f;
            m_audioSource.Play(audioClip);
        }
    }

    /// <summary>
    /// pitchをランダムにして音を鳴らす（繰り返し音を鳴らすものなどに）
    /// </summary>
    public void PlaySePitchRandomize(string clipName, float range = 0.5f)
    {
        var audioClip = m_audioClipList.FirstOrDefault(clip => clip.name == clipName);

        if (audioClip)
        {
            m_audioSource.pitch = Random.Range(1f - range, 1f + range);
            m_audioSource.Play(audioClip);
        }
    }

}
