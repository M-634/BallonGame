using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// test用
/// </summary>
public class Test_LoadTitleSceneButton : MonoBehaviour
{
    public void OnClickButton()
    {
        SceneLoader.Instance.LoadTitleScene();
    }
}
