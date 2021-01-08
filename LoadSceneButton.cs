using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ロードボタン用のスクリプト
/// </summary>
public class LoadSceneButtonInSelect : MonoBehaviour
{
    public void LoadTitleScene()
    {
        SceneLoader.Instance.LoadTitleScene();
    }

    public void LoadGameScene()
    {
        SceneLoader.Instance.LoadGameScene();
    }
}
