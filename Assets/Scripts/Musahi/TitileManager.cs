using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルシーンを管理するクラス
/// </summary>
public class TitileManager : MonoBehaviour
{
    [SerializeField] string m_loadSceneName;
    // Start is called before the first frame update
    void Start()
    {
        SceneLoader.Instance.LoadWithTap(m_loadSceneName);   
    }
}
