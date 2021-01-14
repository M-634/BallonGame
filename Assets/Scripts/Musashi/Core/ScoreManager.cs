using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Collections.LowLevel.Unsafe;

/// <summary>
///ゲーム中のスコアを管理し、ゲームクリアしたらリザルトを表示する
///memo:トータルスコアがランク制に変化した
/// </summary>
public class ScoreManager : EventReceiver<ScoreManager>
{
    [SerializeField] UISetActiveControl m_UISetActiveControl;
    private int m_currentScore = 0;
    private int m_totalCoinNum;
    private int m_getCoinNum;

    /// <summary>
    /// ゲームシーン内のコインの総数を数える関数
    /// </summary>
    public void CountCoinNumber()
    {
        var coinNum = GameObject.FindGameObjectsWithTag("Coin");
        m_totalCoinNum = coinNum.Length;
        Debug.Log("Coin TotalNumber : " + m_totalCoinNum);
    }


    public void GetScoreItem(int score, ItemType itemType)
    {
        m_currentScore += score;
        m_UISetActiveControl.CurrentScoreText.text = "Score: " + m_currentScore;

        if (itemType == ItemType.Coin) m_getCoinNum++;
    }

    /// <summary>
    /// ゲームクリア!
    /// 獲得スコアとクリア時間を表示。
    /// ハイスコアを更新したらセーブする
    /// </summary>
    public void DisplayResult(float clearTime)
    {
        Sequence sequence = DOTween.Sequence();

        float time = 0;
        sequence.Append(
            DOTween.To(() => time, num => time = num, clearTime, 2f)
            .OnUpdate(() => m_UISetActiveControl.ClearTimeScoreText.TimerInfo(time)));

        int score = 0;
        sequence.Append(
        DOTween.To(() => score, num => score = num, m_currentScore, 2f)
            .OnUpdate(() => m_UISetActiveControl.ResulScoreText.text = score.ToString()))
            .OnComplete(() =>
            {
                //ステージ内のコインの総数と獲得したコインの数の割合でランク付け（A～C）
                if (m_totalCoinNum == 0)
                {
                    m_UISetActiveControl.DetermineTheRank(0);
                }
                else
                {
                    int ratio = m_getCoinNum / m_totalCoinNum * 100;
                    m_UISetActiveControl.DetermineTheRank(ratio);
                }
                SaveScoreAndTime(m_currentScore, clearTime);
            });
    }

    private void SaveScoreAndTime(int totalScore, float clearTime)
    {
        if (StageParent.Instance)
        {
            //ステージデータをセーブ
            StageParent.Instance.GetAppearanceStageData.Save(totalScore, clearTime);
            //ステージを非表示にする
            StageParent.Instance.GetAppearanceStagePrefab.SetActive(false);
            //ステージを初期化する
            StageParent.Instance.Initialization();
        }

        if (SceneLoader.Instance)
        {
            //タップしたらセレクト画面に戻る(タップしてください。みたいなテキストを出す)
            SceneLoader.Instance.LoadSceneWithTap();
        }
    }

    protected override void OnEnable()
    {
        m_eventSystemInGameScene.GetItemEvent += GetScoreItem;
    }

    protected override void OnDisable()
    {
        m_eventSystemInGameScene.GetItemEvent -= GetScoreItem;
    }
}
