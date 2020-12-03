using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Jsonを使用したセーブ機能。
/// TODo： セーブするタイミングは ScoreManagerでゲームクリア時のみ！
/// 修正したらもう少しスマートに書けるはず
/// </summary>
#pragma warning disable IDE0063 // Use simple 'using' statement
public class SaveAndLoadWithJSON 
{
    const string FolderName = "SaveData";
    const string EndOfFileName = "_HighScoreData.json";

    static readonly string m_folderPath = Path.Combine(Application.persistentDataPath, FolderName);
    static readonly string m_metaPath = Application.persistentDataPath + $"/{FolderName}.meta ";

    private string m_filepath;


    public void SaveStageData(StageData stageData)
    {
        string json = JsonUtility.ToJson(stageData, true);
        Debug.Log("シリアライズされた JSONデータ" + json);

        //SaveDataフォルダーがないなら作成する
        if (!Directory.Exists(m_folderPath))
        {
            Directory.CreateDirectory(m_folderPath);
            Debug.Log("Initialize folder.....");
        }

        m_filepath = Path.Combine(m_folderPath, stageData.GetStagePath + EndOfFileName);

        //ハイスコアを上書きする。ファイルがなかったら作成して保存する
        using (StreamWriter writer = new StreamWriter(m_filepath, false))
        {
            writer.Write(json);
            Debug.Log("Saving HighScore.....");
        }
    }

    public StageData LoadStageData(StageData stageData)
    {
        m_filepath = Path.Combine(m_folderPath, stageData.GetStagePath + EndOfFileName);

        if (!File.Exists(m_filepath))
        {
            Debug.Log("Initialize file.....");
            stageData.InitializeStageData();
            return stageData;
        }

        using (StreamReader reader = new StreamReader(m_filepath))
        {
            string json = reader.ReadToEnd();
            stageData = JsonUtility.FromJson<StageData>(json);
            Debug.Log("Loading HighScore.....");
            return stageData;
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

        StageParent.Instance.ReLoadStageData();
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
        StageParent.Instance.ReLoadStageData();
    }
#endif
}
#pragma warning restore IDE0063 // Use simple 'using' statement
