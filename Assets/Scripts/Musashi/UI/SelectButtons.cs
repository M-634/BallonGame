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
    int stageNumber = 1;

    /// <summary>
    /// 各ボタンにステージ情報を送り,ステージを解放するかどうかを決める
    /// </summary>
    private void Start()
    {
        foreach (var btn in m_buttons)
        {
            //if (btn.gameObject.activeSelf == false) return;//ボタンのアクティブがfalseだとエラーはくのを防ぐため

            if (m_doReleaseStage)
            {
                btn.SelectButton.interactable = true;
                btn.SetOpenedStageSprite();
            }
            else
            {
                btn.SelectButton.interactable = false;
                btn.SetUnOpenedStageSprite();
            }

            btn.StageNumber = stageNumber;
            //ボタンにステージデータを送る
            btn.StageData = StageParent.Instance.SearchStageData(btn.StageNumber);
            stageNumber++;
          
            if (btn.StageData.IsStageClear)
            {
                //ステージクリアした印を付ける
                //btn.ClearText.gameObject.SetActive(true);
                btn.StageClearImage.gameObject.SetActive(true);
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
