using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// UGUIの拡張クラス
/// </summary>
public static class UGUIListnerExtension 
{
   public static IEnumerator Fadeout(this Graphic graphic ,float fadeTime = 2f,UnityAction callback = null)
    {
        while (graphic.color.a > 0f)
        {
            float temp = graphic.color.a - Time.deltaTime / fadeTime;
            graphic.SetAlpha(temp);
            yield return null;
        }

        callback?.Invoke();
    }

    public static IEnumerator FadeIn(this Graphic graphic,float fadeTime = 2f)
    {
        while (graphic.color.a < 1f)
        {
            float temp = graphic.color.a + Time.deltaTime / fadeTime;
            yield return null;
        }
    }


    public static void SetAlpha(this Graphic graphic ,float alpha)
    {
        graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, alpha);
    }
}
