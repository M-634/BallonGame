using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;

public class CopyFileName : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Assets/Copy FileName",false)]
    static void  Execute()
    {
        //ファイルの名前を取得
        int instanceID = Selection.activeInstanceID;
        string path = AssetDatabase.GetAssetPath(instanceID);
        string fileName = Path.GetFileName(path);
        fileName = fileName.Split('.').FirstOrDefault();

        //クリップボードにコピーする
        GUIUtility.systemCopyBuffer = fileName;
        Debug.Log("Copy clipboard : \n" + fileName);
    }
#endif
}
