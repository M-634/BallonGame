using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// オプションボタンを押したら、設定画面を開き
/// それ以外はポーズする
/// </summary>
public class OptionButtonControler: MonoBehaviour
{
    public void ShowConfigUI()
    {
        ConfigUIManager.Instance.ShowConfigUI();
    } 
}
