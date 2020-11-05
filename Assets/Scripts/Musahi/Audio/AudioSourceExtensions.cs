using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class AudioSourceExtensions
{
    /// <summary>
    /// Nullチェックと音量調整をして音を再生する拡張メソッド
    /// </summary>
    public static void Play(this AudioSource audioSource, AudioClip audioClip = null, float volume = 1f)
    {
        if (audioClip)
        {
            audioSource.clip = audioClip;
            //ボリュームが適切になるように調整する
            audioSource.volume = Mathf.Clamp01(volume);

            audioSource.Play();
        }
    }

    
    public static void PlayWithFadeIn(this AudioSource audioSource,AudioClip audioClip = null,float fadeTime = 0.1f,float endVolume = 1.0f)
    {
        //目標ボリュームを0~1に補正
        float targetVolume = Mathf.Clamp01(endVolume);

        //フェード時間がおかしかったら補正
        fadeTime = fadeTime < 0.1f ? 0.1f : fadeTime;

        //音量0で再生開始
        audioSource.Play(audioClip, 0f);

        //DOTweenを使って目標ボリュームまでFade
        audioSource.DOFade(targetVolume, fadeTime);
    }

    public static void PlayWithFadeOut(this AudioSource audioSource,float fadeTime = 0.1f)
    {
      
        //フェード時間がおかしかったら補正
        fadeTime = fadeTime < 0.1f ? 0.1f : fadeTime;

        audioSource.DOFade(0f, fadeTime);
        audioSource.Stop();
        audioSource.clip = null;
    }

    /// <summary>
    /// 再生位置をランダムにする拡張メソッド（環境音などに）
    /// </summary>
    public static void PlayRandomStart(this AudioSource audioSource,AudioClip audioClip,float volume = 1f)
    {
        if (audioClip == null) return;

        audioSource.clip = audioClip;
        audioSource.volume = Mathf.Clamp01(volume);

        //結果がlengthと同値になるとシークエラーを起こすため -0.01秒する
        audioSource.time = Random.Range(0f, audioClip.length - 0.01f);

        PlayWithFadeIn(audioSource, audioClip, volume);
    }
}
