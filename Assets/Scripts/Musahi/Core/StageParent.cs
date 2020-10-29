using System;
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
    /// <summary>ステージプレハブを登録する</summary>
    [SerializeField] GameObject[] m_stagePrefabs;
    /// <summary>インスタンス化したstagePrefabが入ったObject</summary>
    private readonly List<GameObject> m_stageDateList = new List<GameObject>();
    [SerializeField] GameObject m_getAppearanceStage;//serializeはDebug用
    /// <summary>出現させるステージプレハブ </summary>
    public GameObject GetAppearanceStage { get => m_getAppearanceStage; }
    /// <summary>ステージの天候状態</summary>
    public WeatherConditions WeatherConditions { get; set; }
    /// <summary>ハイスコアをセーブするパス</summary>
    public string FullPath { get; private set; }

    private void Start()
    {
        foreach (var stage in m_stagePrefabs)
        {
            var go = Instantiate(stage);
            go.transform.SetParent(this.transform);
            go.SetActive(false);
            m_stageDateList.Add(go);
        }
        Initialization();
    }

    public void Initialization()
    {
        m_getAppearanceStage = null;
        WeatherConditions = WeatherConditions.None;
        FullPath = "";
    }

    /// <summary>
    /// ステージセレクトボタンを押した時に、次のゲームシーンで出現させる
    /// ステージ情報を確定させる
    /// </summary>
    public void SetStageInfo(GameObject stage, WeatherConditions conditions, Action callback)
    {
        if (stage == null)
        {
            Debug.LogError("ステージプレハブがセットされていません！！");
            return;
        }

        //stageを検索
        int index = -1;
        for (int i = 0; i < m_stagePrefabs.Length; i++)
        {
            if (m_stagePrefabs[i].Equals(stage))
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            Debug.LogError("ステージリストに指定したプレハブが存在しません!!");
            return;
        }

        //stageをセットする
        m_getAppearanceStage = m_stageDateList[index];


        //天候をセットする
        WeatherConditions = conditions;

        //pathを指定する
#if UNITY_ANDROID
        m_fullPath = Application.streamingAssetsPath + $"/{stage.name}_HighScoreData.json";  
#else
        FullPath = Application.dataPath + $"/{stage.name}_HighScoreData.json";
#endif

        //ゲームシーンをロード
        callback?.Invoke();
    }

}
