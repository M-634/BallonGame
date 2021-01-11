using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// UGUIの拡張クラス
/// </summary>
public static class UGUIListnerExtension 
{
    /// <summary>
    /// 時間を00:00形式でUIに表示する拡張クラス
    /// </summary>
    /// <param name="timerText"></param>
    /// <param name="time"></param>
    public static void TimerInfo(this Text timerText, int time) 
    {
        int minute = time / 60;
        float seconds = time - minute * 60;
        timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
    }

    /// <summary>
    /// TextMeshPro版
    /// </summary>
    /// <param name="timerText"></param>
    /// <param name="time"></param>
    public static void TimerInfo(this TextMeshProUGUI timerText, int time)
    {
        int minute = time / 60;
        float seconds = time - minute * 60;
        timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
    }

    public static void Show(this CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = true;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public static void Hide(this CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }

    public static void SetValueChangedEvent(this Slider slider,UnityAction<float> sliderCallback)
    {
        if (slider.onValueChanged != null)
        {
            slider.onValueChanged.RemoveAllListeners();
        }

        slider.onValueChanged.AddListener(sliderCallback);
    }

   //public static IEnumerator FadeOut(this Graphic graphic ,float fadeTime = 2f,UnityAction callback = null)
   // {
   //     while (graphic.color.a > 0f)
   //     {
   //         float temp = graphic.color.a - Time.deltaTime / fadeTime;
   //         graphic.SetAlpha(temp);
   //         yield return null;
   //     }
   //     graphic.DOColor(Color.clear, fadeTime);
   //     callback?.Invoke();
   // }

   // public static IEnumerator FadeIn(this Graphic graphic,float fadeTime = 2f)
   // {
   //     while (graphic.color.a < 1f)
   //     {
   //         float temp = graphic.color.a + Time.deltaTime / fadeTime;
   //         graphic.SetAlpha(temp);
   //         yield return null;
   //     }
   //     graphic.DOColor(Color.black, fadeTime);
   // }

    public static void SetAlpha(this Graphic graphic ,float alpha)
    {
        graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, alpha);
    }

    public static void FadeOutWithDoTween(this Graphic graphic, float fadeTime,UnityAction callback = null)
    {
        graphic.DOColor(Color.clear, fadeTime).OnComplete(() => callback?.Invoke());
    }

    public static void FadeInWithDoTween(this Graphic graphic,float fadeTime,UnityAction callback = null)
    {
        graphic.DOColor(Color.black, fadeTime).OnComplete(() => callback?.Invoke());
    }
}
