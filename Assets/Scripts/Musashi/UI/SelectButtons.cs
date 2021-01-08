using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// セレクトシーンの選択ボタンをまとめて管理するクラス
/// </summary>
public class SelectButtons : MonoBehaviour
{
    /// <summary>セレクトボタンをセットする</summary>
    [SerializeField] SelectStageButton[] m_buttons;
    /// <summary>ステージ解放フラグ </summary>
    bool m_doReleaseStage = true;//初めのステージは必ず解放する

    /// <summary>
    /// 各ボタンにステージ情報を送り,ステージを解放するかどうかを決める
    /// </summary>
    private void Start()
    {
        foreach (var btn in m_buttons)
        {
            btn.StageData = StageParent.Instance.SendStageData(btn.StagePrefab);
            if (m_doReleaseStage)
            {
                btn.SelectButton.interactable = true;
            }
            else
            {
                btn.SelectButton.interactable = false;
            }

            if (btn.StageData.IsStageClear)
            {
                //クリアテキストをボタンに表示
                btn.ClearText.gameObject.SetActive(true);
            }
            else
            {
                //ステージクリアしていないなら、次のステージ以降の
                //セレクトボタンは解放しない
                m_doReleaseStage = false;
            }
        }
    }
}
