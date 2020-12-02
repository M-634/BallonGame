using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//memo;
//Sunny,
//ThunderStorm,
//Hurricane
public class WeatherSystem : MonoBehaviour
{
    [Header("SkyBoxをアサインしてね！")]
    [SerializeField] Material m_sunnySkyBox;
    [SerializeField] Material m_thunderStormSkyBox;
    [SerializeField] Material m_hurricaneSkybox;

    private void Start()
    {
        //skyBoxをセットする
        if (StageParent.Instance)
        {
            //switch (StageParent.Instance.WeatherConditions)
            //{
            //    case WeatherConditions.Initialize:
            //        break;
            //    case WeatherConditions.Sunny:
            //        RenderSettings.skybox = m_sunnySkyBox;
            //        break;
            //    case WeatherConditions.ThunderStorm:
            //        RenderSettings.skybox = m_thunderStormSkyBox;
            //        break;
            //    case WeatherConditions.Hurricane:
            //        RenderSettings.skybox = m_hurricaneSkybox;
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
