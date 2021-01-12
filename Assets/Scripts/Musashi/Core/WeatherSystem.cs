using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//memo;
//Sunny,
//ThunderStorm,
//Hurricane
/// <summary>
/// ゲームシーンでの天候（skybox）をセットする。
/// 天候に応じて、ステージギミック、Shadr等を制御するクラス
/// To Do: ここStatePatternに変更する
/// </summary>
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
            switch (StageParent.Instance.GetAppearanceStageData.Conditons)
            {
                case StageData.WeatherConditons.Initialize:
                    break;
                case StageData.WeatherConditons.Sunny:
                    RenderSettings.skybox = m_sunnySkyBox;
                    break;
                case StageData.WeatherConditons.ThunderStorm:
                    RenderSettings.skybox = m_thunderStormSkyBox;
                    break;
                case StageData.WeatherConditons.Hurricane:
                    RenderSettings.skybox = m_hurricaneSkybox;
                    break;
                default:
                    break;
            }
        }
    }
}
