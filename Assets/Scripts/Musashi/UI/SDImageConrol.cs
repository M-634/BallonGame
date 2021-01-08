using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SDキャラクターのアニメーション表示を、ゲームのクリア状態に応じて切り替えるクラス
/// </summary>
public class SDImageConrol : MonoBehaviour
{
    [SerializeField] ImageAnimation[] m_animationSDImages;
    [Header("ロード遷移で使用する時はチェックを入れること！！")]
    [SerializeField] bool m_useOnLoad;

    public void Start()
    {
        if (m_useOnLoad) return;

        DisplaySDImage();//セレクトシーン内ならStart関数で呼ぶ
    }

    public void DisplaySDImage()
    {
        foreach (var image in m_animationSDImages)
        {
            if (image.GameClearState == StageParent.Instance.GameClearState)
            {
                image.gameObject.SetActive(true);
                return;
            }
        }
    }
}


/// <summary>
/// ゲームのクリア状態
/// Memo:セレクトシーンやロード遷移でSDImageの表示を変えるため)
/// </summary>
public enum GameClearState
{
    None, GameClear, GameOver
}




