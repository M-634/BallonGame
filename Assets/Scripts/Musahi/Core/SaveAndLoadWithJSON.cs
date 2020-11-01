using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class StageData
{
    public int HighScore;
    public bool IsStageClear;
}

/// <summary>
/// Jsonを使用したセーブ機能。
/// ステージ名でパスを振り分ける
/// </summary>
public class SaveAndLoadWithJSON
{
    readonly string m_path;
    StageData m_StageData;

    /// <summary>
    /// test用のコンストラクター
    /// </summary>
    public SaveAndLoadWithJSON()
    {
        m_path = Application.dataPath + $"/Test_HighScoreData.json";
        m_StageData = new StageData();
    }

    /// <summary>
    /// パスを指定するコンストラクター
    /// </summary>
    /// <param name="path">ステージ名 ＋ 天候状態の名前</param>
    public SaveAndLoadWithJSON(string path)
    {
#if UNITY_ANDROID
        m_path = Application.streamingAssetsPath + $"/{path}_HighScoreData.json";  
#elif UNITY_EDITOR
        m_path = Application.dataPath + $"/{path}_HighScoreData.json";
#endif
        m_StageData = new StageData();
    }

    public void SaveHighScore(int score,bool isClear)
    {
        m_StageData.IsStageClear = isClear;
        m_StageData.HighScore = score;
        string json = JsonUtility.ToJson(m_StageData, true);
        Debug.Log("シリアライズされた JSONデータ" + json);

        //ハイスコアを上書きする
        StreamWriter writer = new StreamWriter(m_path, false);
        writer.Write(json);
        writer.Flush();
        writer.Close();
        Debug.Log("Saving HighScore.....");
    }



    public int LoadHighScore()
    {
        if (!File.Exists(m_path))
        {
            //make file
            SaveHighScore(0,false);
            Debug.Log("Initialize file.....");
            return 0;
        }

        StreamReader reader = new StreamReader(m_path);
        string json = reader.ReadToEnd();
        reader.Close();
        m_StageData = JsonUtility.FromJson<StageData>(json);
        Debug.Log("Loading HighScore.....");
        return m_StageData.HighScore;
    }

    public bool CheakStageClear()
    {
        if (!File.Exists(m_path))
        {
            return false;
        }

        StreamReader reader = new StreamReader(m_path);
        string json = reader.ReadToEnd();
        reader.Close();
        m_StageData = JsonUtility.FromJson<StageData>(json);
        return m_StageData.IsStageClear;
    }
}
