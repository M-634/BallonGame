using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// test用
/// </summary>
public class Test_LoadSceneButton : MonoBehaviour
{
    public void LoadTitleScene()
    {
        SceneLoader.Instance.LoadTitleScene();
    }

    ///// <summary>
    ///// はじめから（SaveDataを消す）
    ///// </summary>
    //public void LoadSelectScene()
    //{
    //    SaveAndLoadWithJSON.DeleteSaveData();
    //    SceneLoader.Instance.LoadSelectScene();
    //}

    ///// <summary>
    ///// 続きから
    ///// </summary>
    //public void LoadSelectSceneWithContinue()
    //{
    //    SceneLoader.Instance.LoadSelectScene();
    //}
}
