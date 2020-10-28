using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージの天候状態
/// </summary>
public enum WeatherConditions
{
    None,
    Sunny,
    ThunderStorm,
    Hurricane
}
/// <summary>
/// ステージ情報を管理する
/// セレクト画面でボタンを押した時に確定させ、
/// 次のゲームシーンで参照させる
/// </summary>
public class StageParent : SingletonMonoBehavior<StageParent>
{
    /// <summary>ステージプレハブリスト</summary>
    [SerializeField] List<GameObject> m_stagePrefabsList;

    /// <summary>出現させるステージ</summary>
    private GameObject m_stageObject;
    /// <summary>ステージの天候状態</summary>
    public WeatherConditions WeatherConditions { get; set; }

    /// <summary>ハイスコアをセーブするパス</summary>
    public string FullPath { get; private set; }

    private void Start()
    {
        foreach (var stage in m_stagePrefabsList)
        {
            var go = Instantiate(stage);
            go.transform.SetParent(this.transform);
            go.SetActive(false);
        }
        Initialization();
    }

    public void Initialization()
    {
        m_stageObject = null;
        WeatherConditions = WeatherConditions.None;
        FullPath = "";
    }

    /// <summary>
    /// セレクト画面から呼ばれる関数
    /// セレクトボタンスクリプトでステージプレハブと天候をセットしておく
    /// </summary>
    public void SetStageInfo(GameObject stage, WeatherConditions conditions)
    {
        if (stage == null)
        {
            Debug.LogError("ステージプレハブがセットされていません！！");
            return;
        }

        bool isStage = m_stagePrefabsList.Contains(stage);

        if (!isStage)
        {
            Debug.LogError("ステージリストに指定したプレハブが存在しません!!");
            return;
        }
        else
        {
            m_stageObject = stage;
        }

        WeatherConditions = conditions;

        //pathを指定する
#if UNITY_ANDROID
        m_fullPath = Application.streamingAssetsPath + $"/{stage.name}_HighScoreData.json";  
#else
        FullPath = Application.dataPath + $"/{stage.name}_HighScoreData.json";
#endif
    }
     
    public GameObject GetStagePrefab()
    {
        return m_stageObject;
    }



}
