
///*********
/// memo : stageに持たせるべき情報
/// masterData : 天候情報、タイムリミット,ステージPrefab,ステージの動画,セーブのPath
/// saveData : ハイスコア、クリアタイム、クリアしたかどうかの判定、
///*********

using System;
using UnityEngine;

[Serializable]
public class StageData
{
    //memo; Master Dataもシリアライズされる......さして問題が、別にシリアライズする必要が無いからなぁ～。
    //将来的に内部クラスでも持たせて、そいつだけシリアライズする方針を検討中
    #region Master Data
    [SerializeField] GameObject m_stagePrefab;
    [SerializeField] WeatherConditons m_weatherConditons;
    [SerializeField] float m_timeLimit = 300f;
    [SerializeField] int m_stageNumber;
    #endregion

    #region Master Data Property
    public GameObject StagePrefab { get => m_stagePrefab; }
    public WeatherConditons Conditons { get => m_weatherConditons; }
    public float SetTimeLimit { get => m_timeLimit; }
    public string GetStagePath { get => m_stagePrefab.name; }
    public int StageNumber { get => m_stageNumber; }
    #endregion

    #region Save Data
    [SerializeField,HideInInspector]
    private int m_highScore = 0;
    [SerializeField,HideInInspector]
    private float m_clearTime = 0;
    [SerializeField,HideInInspector]
    private bool m_isStageClear = false;
    #endregion

    #region SaveDataProperty
    public int HighScore { get => m_highScore; }
    public float ClearTime { get => m_clearTime;}
    public bool IsStageClear { get => m_isStageClear; }
    #endregion

    #region Method
    public void InitializeStageData()
    {
        m_highScore = 0;
        m_clearTime = 0;
        m_isStageClear = false;
    }

    public void Save(int score, float claerTime)
    {
        if (score > m_highScore)
        {
            m_highScore = score;
        }
        m_clearTime = claerTime;
        m_isStageClear = true;

        var json = new SaveAndLoadWithJSON();
        json.SaveStageData(this);
    }
    #endregion 
    public enum WeatherConditons
    {
        Initialize, Sunny, ThunderStorm, Hurricane
    }
}

