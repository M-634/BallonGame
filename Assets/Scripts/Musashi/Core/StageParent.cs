using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ情報を管理する
/// セレクト画面でボタンを押した時に確定させ、
/// 次のゲームシーンで参照させる
/// </summary>
public class StageParent : SingletonMonoBehavior<StageParent>
{
    /// <summary>ステージプレハブを登録する(Projectからアサインすること)</summary>
    [SerializeField] GameObject[] m_stagePrefabs;
    /// <summary>インスタンス化したstagePrefabが入ったリスト(Hierarchy上に生成されたプレハブ)</summary>
    private readonly List<GameObject> m_stageDateList = new List<GameObject>();
    /// <summary>出現させるステージ </summary>
    public GameObject GetAppearanceStage { get; private set; }
    /// <summary>ステージの天候状態</summary>
    public WeatherConditions WeatherConditions { get; set; }
    /// <summary>ステージ名</summary>
    public string StageName { get; set; }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //最初のダミーシーンでステージを生成して、アクティブを非表示にする
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
        WeatherConditions = WeatherConditions.Initialize;
    }

    /// <summary>
    /// ステージセレクトボタンを押した時に、次のゲームシーンで出現させる
    /// ステージ情報を確定させる
    /// </summary>
    public void SetStageInfo(GameObject stage, WeatherConditions conditions)
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
        //名前をセットする
        StageName = stage.name;
        //ゲームシーンをロード
        SceneLoader.Instance.LoadGameScene();
    }


    public void AppearanceStageObject(Transform stageParent)
    {
        //ステージの非表示な子オブジェクトを表示する
        foreach (Transform item in stageParent)
        {
            if (item.gameObject.activeSelf == false)
            {
                item.gameObject.SetActive(true);
            }
            //さらに子供が非表示なら表示する
            AppearanceStageObject(item);
        }
        GetAppearanceStage.SetActive(true);
    }

    //public void SetActiveStageObjs(Transform obj)
    //{
    //    foreach (Transform item in obj)
    //    {
    //        if (item.gameObject.activeSelf == false)
    //        {
    //            item.gameObject.SetActive(true);
    //        }
    //    }
    //}
}
