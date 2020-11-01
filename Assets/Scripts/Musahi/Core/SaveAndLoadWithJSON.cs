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
    string m_folderPath;
    string m_filepath;

    StageData m_StageData;

    /// <summary>
    /// test用のコンストラクター
    /// </summary>
    public SaveAndLoadWithJSON()
    {
        m_folderPath = Path.Combine(Application.dataPath, "SaveData");
        m_filepath = Application.dataPath + $"/Test_HighScoreData.json";
        m_StageData = new StageData();
    }

    /// <summary>
    /// パスを指定するコンストラクター
    /// </summary>
    /// <param name="path">ステージ名 ＋ 天候状態の名前</param>
    public SaveAndLoadWithJSON(string path)
    {

#if UNITY_ANDROID
        m_folderPath = Path.Combine(Application.streamingAssetsPath, "SaveData");
        m_filepath = Path.Combine(m_folderPath, $"{path}_HighScoreData.json");
#elif UNITY_EDITOR
        m_folderPath = Path.Combine(Application.dataPath, "SaveData");
        m_filepath = Path.Combine(m_folderPath, $"{path}_HighScoreData.json");
#endif
        m_StageData = new StageData();
    }

    public void SaveHighScore(int score, bool isClear)
    {
        m_StageData.IsStageClear = isClear;
        m_StageData.HighScore = score;
        string json = JsonUtility.ToJson(m_StageData, true);
        Debug.Log("シリアライズされた JSONデータ" + json);

        //SaveDataフォルダーがないなら作成する
        if (!Directory.Exists(m_folderPath))
        {
            Directory.CreateDirectory(m_folderPath);
            Debug.Log("Initialize folder.....");
        }

        //ハイスコアを上書きする
        StreamWriter writer = new StreamWriter(m_filepath, false);
        writer.Write(json);
        writer.Flush();
        writer.Close();
        Debug.Log("Saving HighScore.....");
    }



    public int LoadHighScore()
    {
        if (!File.Exists(m_filepath))
        {
            //make file
            SaveHighScore(0, false);
            Debug.Log("Initialize file.....");
            return 0;
        }

        StreamReader reader = new StreamReader(m_filepath);
        string json = reader.ReadToEnd();
        reader.Close();
        m_StageData = JsonUtility.FromJson<StageData>(json);
        Debug.Log("Loading HighScore.....");
        return m_StageData.HighScore;
    }

    public bool CheakStageClear()
    {
        if (!File.Exists(m_filepath))
        {
            return false;
        }

        StreamReader reader = new StreamReader(m_filepath);
        string json = reader.ReadToEnd();
        reader.Close();
        m_StageData = JsonUtility.FromJson<StageData>(json);
        return m_StageData.IsStageClear;
    }

    /// <summary>
    /// SaveDataフォルダーを削除する関数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void DeletSaveData()
    {
        if (!Directory.Exists(m_folderPath))
        {
            Debug.Log(m_folderPath + "にフォルダーは存在しません");
            return;
        }

        //フォルダーに存在するjsonファイルを全て削除する
        Directory.Delete(m_folderPath, true);
    }
}
