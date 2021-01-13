using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ステージ情報を管理するクラス
/// </summary>
public class StageParent : SingletonMonoBehavior<StageParent>
{
    /// <summary>ステージデータ</summary>
    [SerializeField] StageData[] m_stageDatas;
    /// <summary>Hierarchy上に生成されたステージプレハブ</summary>
    private readonly List<GameObject> m_stagePrefabList = new List<GameObject>();
    /// <summary>出現させるステージデータ </summary>
    public StageData GetAppearanceStageData { get; private set; }
    /// <summary>出現させるステージプレハブ</summary>
    public GameObject GetAppearanceStagePrefab { get; private set; }
    /// <summary>ステージをクリアしたかどうかの状態を伝える</summary>
    public GameClearState GameClearState { get; set; }

    /// <summary>stageDatasからstagePrefabだけ取り出す</summary>
    ///<remarks>
    ///Instantiate関数の引数がオリジナルなGameObject型変数を指定しないとエラーが起こるため,
    ///この配列を用意した。
    ///58行目のコメントアウトを参照してください
    ///</remarks> 
    private GameObject[] m_stageObjs;
 
    protected override void Awake()
    {
        base.Awake();
        GameClearState = GameClearState.None;

        m_stageObjs = new GameObject[m_stageDatas.Length];
        for (int i = 0; i < m_stageDatas.Length; i++)
        {
            m_stageObjs[i] = m_stageDatas[i].StagePrefab;
            m_stageDatas[i].StageNumber = i + 1 ;
            Debug.Log(m_stageDatas[i].StageNumber);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void ReLoadStageData()
    {
        var json = new SaveAndLoadWithJSON();
        for (int i = 0; i < m_stageDatas.Length; i++)
        {
            //各ステージデータをロードする
            m_stageDatas[i] = json.LoadStageData(m_stageDatas[i]);
        }
    }

    private void Start()
    {
        ReLoadStageData();
        //最初のダミーシーンでステージを生成して、アクティブを非表示にする
        //foreach (var data in m_stageDatas)
        //{
        //    var go = Instantiate(data.StagePrefab);//invalidCastException: Specified cast is not valid.
        //    go.transform.SetParent(this.transform);
        //    go.SetActive(false);
        //    m_stagePrefabList.Add(go);
        //}
        for (int i = 0; i < m_stageDatas.Length; i++)
        {
            var go = Instantiate(m_stageObjs[i]);
            go.transform.SetParent(this.transform);
            go.SetActive(false);
            m_stagePrefabList.Add(go);
        }
        Initialization();
    }

    public void Initialization()
    {
        GetAppearanceStageData = null;
        GetAppearanceStagePrefab = null;
    }

    /// <summary>
    /// 各ステージセレクトボタンにアタッチされている
    /// SelectGameSceneButtonにStageDataを渡す
    /// 検索をステージ番号に変更する
    /// </summary>
    public StageData SearchStageData(int stageNumber)
    {
        //stageDataを検索
        //for (int i = 0; i < m_stageDatas.Length; i++)
        //{
        //    if (m_stageObjs[i] == stagePrefab)
        //    {
        //        return m_stageDatas[i];
        //    }
        //}
        //return null;
        for (int i = 0; i < m_stageDatas.Length; i++)
        {
            if (m_stageDatas[i].StageNumber == stageNumber)
            {
                return m_stageDatas[i];
            }
        }
        return null;
    }

    /// <summary>
    /// ステージセレクトボタンをタップした時に、次のゲームシーンで出現させる
    /// ステージ情報をセットする
    /// </summary>
    public void SetStageInfo(int stageNumber)
    {
        //stageを検索
        for (int i = 0; i < m_stageDatas.Length; i++)
        {
            if(m_stageDatas[i].StageNumber == stageNumber)
            {
                //stageをセットする
                GetAppearanceStageData = m_stageDatas[i];
                GetAppearanceStagePrefab = m_stagePrefabList[i];
                return;
            }
        }
        Debug.LogWarning("該当するStagePrefabがStageDataに存在しません");
    }

    /// <summary>
    /// ゲームシーンで出すStagePrefabのアクティブをtrueにする
    /// </summary>
    /// <param name="stageParent"></param>
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
        GetAppearanceStagePrefab.SetActive(true);
    }
}
