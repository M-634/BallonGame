using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Audio;

public static class AudioMixerExtensions
{
    //UnityのAudioMixerはDecibelでボリュームを設定するので
    //0f~1.0fのLinear値に変換してSetしたりGetしたりする関数
    //以下参考リンク
    //https://gist.github.com/TakaakiIchijo/69eefdc44183f4ddea035a7fee8ef0ac

    //decibelから0 ~ 1.0fのLinear値へ変換
    public static float GetVolumeByLinear(this AudioMixer audioMixer,string exposedParamName)
    {
        audioMixer.GetFloat(exposedParamName, out float decibel);

        if (decibel <= -96)
        {
            return 0.0f;
        }
        return Mathf.Pow(10f, decibel / 20f);
    }

    //0~1.0fのLinear値からdecibelへ変換
    public static void SetVolumeByLinear(this AudioMixer audioMixer,string exposedParamName,float volume)
    {
        float decibel = 20.0f * Mathf.Log10(volume);

        if (float.IsNegativeInfinity(decibel))
        {
            decibel = -96f;
        }

        audioMixer.SetFloat(exposedParamName, decibel);
    }
}