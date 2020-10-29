using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スタートボタン（test）
/// </summary>
[RequireComponent(typeof(Button))]
public class Test_Button : MonoBehaviour
{
    public void SceneLoad(string m_loadSceneName)
    {
        SceneLoader.Instance.Load(m_loadSceneName);
    }
}
