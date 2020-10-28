using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// セレクト画面のボタンにアタッチするクラス
/// </summary>
[RequireComponent(typeof(Button))]
public class SelectGameSceneButton : MonoBehaviour
{
    [SerializeField] GameObject m_stagePrefab;
    [SerializeField] WeatherConditions m_conditions;


    public void SceneLoad(string m_loadSceneName)
    {
        StageParent.Instance.SetStageInfo(m_stagePrefab, m_conditions);
        SceneLoader.Instance.Load(m_loadSceneName);
    }
}
