using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Jsonを使用したセーブ機能。
/// </summary>
#pragma warning disable IDE0063 // Use simple 'using' statement
public class SaveAndLoadWithJSON 
{
    const string FolderName = "SaveData";
    const string EndOfFileName = "_HighScoreData.json";

    static readonly string m_folderPath = Path.Combine(Application.persistentDataPath, FolderName);
    static readonly string m_metaPath = Application.persistentDataPath + $"/{FolderName}.meta ";

    readonly string m_filepath;

    StageData m_StageData = new StageData();

    /// <summary>
    /// test用のコンストラクター
    /// </summary>
    public SaveAndLoadWithJSON()
    {
        m_filepath = Path.Combine(m_folderPath, "Test" + EndOfFileName);
    }

    /// <summary>
    /// パスを指定するコンストラクター
    /// </summary>
    /// <param name="path">ステージ名 ＋ 天候状態の名前</param>
    public SaveAndLoadWithJSON(string path)
    {
        m_filepath = Path.Combine(m_folderPath, path + EndOfFileName);
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

        //ハイスコアを上書きする。ファイルがなかったら作成して保存する
        using (StreamWriter writer = new StreamWriter(m_filepath, false))
        {
            writer.Write(json);
            //writer.Flush();
            //writer.Close();
            Debug.Log("Saving HighScore.....");
        }
    }

    public int LoadHighScore()
    {
        //ファイルが存在しないか、テスト用のファイルパスを指定していたらハイスコアを０でセーブして値を返す
        if (!File.Exists(m_filepath) || m_filepath == Path.Combine(m_folderPath, "Test" + EndOfFileName))
        {
            //make file
            SaveHighScore(0, false);
            Debug.Log("Initialize file.....");
            return 0;
        }

        using (StreamReader reader = new StreamReader(m_filepath))
        {
            string json = reader.ReadToEnd();
            //reader.Close();
            m_StageData = JsonUtility.FromJson<StageData>(json);
            Debug.Log("Loading HighScore.....");
            return m_StageData.HighScore;
        }
    }

    public bool CheakStageClear()
    {
        if (!File.Exists(m_filepath))
        {
            return false;
        }

        using (StreamReader reader = new StreamReader(m_filepath))
        {
            string json = reader.ReadToEnd();
            //reader.Close();
            m_StageData = JsonUtility.FromJson<StageData>(json);
            return m_StageData.IsStageClear;
        }
    }

    public static void DeleteSaveData()
    {
        if (!Directory.Exists(m_folderPath))
        {
            Debug.Log(FolderName + "はありません");
            return;
        }

        //フォルダーに存在するjsonファイルを全て削除する
        Directory.Delete(m_folderPath, true);
        if (File.Exists(m_metaPath))
        {
            File.Delete(m_metaPath);//Unityではメタファイルを消すことが重要である。
        }
        Debug.Log("セーブデータを破棄しました！");
    }

#if UNITY_EDITOR
    /// <summary>
    /// エディターでセーブデータを削除するデバック用の関数
    /// メニューバー＞M-634 ＞DeletSaveDataを押すと実行される
    /// </summary>
    [MenuItem("M-634/DeletSaveData")]
    public static void DeleteSaveDatebyEditor()
    {
        if (!Directory.Exists(m_folderPath))
        {
            Debug.Log(FolderName + "はありません");
            return;
        }

        //フォルダーに存在するjsonファイルを全て削除する
        Directory.Delete(m_folderPath, true);
        if (File.Exists(m_metaPath))
        {
            File.Delete(m_metaPath);//Unityではメタファイルを消すことが重要である。
        }
        Debug.Log("セーブデータを破棄しました！");
    }
#endif
}
#pragma warning restore IDE0063 // Use simple 'using' statement
