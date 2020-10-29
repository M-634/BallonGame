using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class HighScoreData
{
    public int HighScore;
}

/// <summary>
/// Jsonを使用したセーブ機能。
/// ステージ名でパスを振り分ける
/// </summary>
public class SaveAndLoadWithJSON 
{
    readonly string m_path;
    HighScoreData m_highScoreData;

    /// <summary>
    /// test用のコンストラクター
    /// </summary>
    public SaveAndLoadWithJSON()
    {
        m_path = Application.dataPath + $"/Test_HighScoreData.json";
        m_highScoreData = new HighScoreData();
    }

    public SaveAndLoadWithJSON(string path)
    {
#if UNITY_ANDROID
        m_path = Application.streamingAssetsPath + $"/{path}_HighScoreData.json";  
#else
        m_path = Application.dataPath + $"/{path}_HighScoreData.json";
#endif
        m_highScoreData = new HighScoreData();
    }

    public void SaveHighScore(int score)
    {
        m_highScoreData.HighScore = score;//error
        string json = JsonUtility.ToJson(m_highScoreData, true);
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
            SaveHighScore(0);
            Debug.Log("Initialize file.....");
            return 0;
        }

        StreamReader reader = new StreamReader(m_path);
        string json = reader.ReadToEnd();
        reader.Close();
        m_highScoreData = JsonUtility.FromJson<HighScoreData>(json);
        Debug.Log("Loading HighScore.....");
        return m_highScoreData.HighScore;
    }
}
