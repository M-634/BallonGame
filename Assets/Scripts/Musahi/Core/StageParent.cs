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

/*
 このクラスは大いに修正すべきだ、セレクトからゲームシーンへどの用にStage情報を
持っていくか考えなおす必要があるだろう
 */
/// <summary>
/// ステージ情報を管理する
/// セレクト画面でボタンを押した時に確定させ、
/// 次のゲームシーンで参照させる
/// </summary>
public class StageParent : SingletonMonoBehavior<StageParent>
{
    /// <summary>ステージプレハブを登録する</summary>
    [SerializeField] GameObject[] m_stagePrefabs;
    /// <summary>インスタンス化したstagePrefabが入ったリスト</summary>
    private readonly List<GameObject> m_stageDateList = new List<GameObject>();
    /// <summary>出現させるステージプレハブ </summary>
    public GameObject GetAppearanceStage { get; private set; }
    /// <summary>ステージの天候状態</summary>
    public WeatherConditions WeatherConditions { get; set; }

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
        GetAppearanceStage = null;
        WeatherConditions = WeatherConditions.None;
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
        GetAppearanceStage = m_stageDateList[index];


        //天候をセットする
        WeatherConditions = conditions;

        //ゲームシーンをロード
        callback?.Invoke();
    }


    public void AppearanceStageObject()
    {
        //ステージの非表示な子オブジェクトを表示する
        foreach (Transform item in GetAppearanceStage.transform)
        {
            if (item.gameObject.activeSelf == false)
            {
                item.gameObject.SetActive(true);
            }
        }
        GetAppearanceStage.SetActive(true);
    }
}
