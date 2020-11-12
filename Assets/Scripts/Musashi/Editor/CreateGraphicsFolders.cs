using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;//拡張エディターを作成するのに必要

/// <summary>
/// デザイナーさん用のフォルダーを作成するクラス
/// </summary>
public class CreateGraphicsFolders : Editor
{
#if UNITY_EDITOR
    static int m_customFolerNumber = 0;

    [MenuItem("M-634/Create/GraphicsFolders")]
    public static void Create()
    {
        string graphicsfolderPath = Path.Combine(Application.dataPath, "Graphics");

        //Graphicsフォルダーを作成する
        if (!Directory.Exists(graphicsfolderPath))
        {
            //初めて作成した
            Directory.CreateDirectory(graphicsfolderPath);
            m_customFolerNumber = 0;
            //Debug.Log("Initialize GraphiscsFolders....");
        }

        string customFolderPath;
        if (m_customFolerNumber == 0)
        {
            customFolderPath = Path.Combine(graphicsfolderPath, "Custom");
        }
        else
        {
            customFolderPath = Path.Combine(graphicsfolderPath, "Custom" + m_customFolerNumber.ToString());
        }
        m_customFolerNumber++;
        Directory.CreateDirectory(customFolderPath);

        string[] pathes = { Path.Combine(customFolderPath, "Textures"),Path.Combine(customFolderPath, "Materials"),
            Path.Combine(customFolderPath, "Meshes"), Path.Combine(customFolderPath, "Prefabs") };

        foreach (var path in pathes)
        {
            Directory.CreateDirectory(path);
        }
        //Debug.Log("実行されてるよ");
        AssetDatabase.Refresh();
    }
#endif
}