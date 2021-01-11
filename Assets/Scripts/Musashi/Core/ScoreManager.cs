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

    private void Start()
    {
        var coinNum = GameObject.FindGameObjectsWithTag("Coin");
        m_totalCoinNum = coinNum.Length;
        Debug.Log("Coin TotalNumber : " + m_totalCoinNum);
    }

    /// <summary>
    /// プレイヤーがコインに衝突したら呼ばれる関数
    /// </summary>
    public void GetCoin(int score)
    {
        m_currentScore += score;
        m_getCoinNum++;
        m_UISetActiveControl.CurrentScoreText.text = "Score: " + m_currentScore;

    }

    /// <summary>
    /// ゲームクリア!
    /// 獲得スコアとクリア時間を表示。
    /// ハイスコアを更新したらセーブする
    /// </summary>
    public void Result(float clearTime)
    {     
        int score = 0;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(
        DOTween.To(() => score, num => score = num, m_currentScore, 2f)
            .OnUpdate(() => m_UISetActiveControl.ResulScoreText.text = score.ToString()))
            .OnComplete(() => Debug.Log("")) ;

        float time = 0;
        sequence.Append(
            DOTween.To(() => time, num => time = num, clearTime, 2f)
            .OnUpdate(() => m_UISetActiveControl.TimerText.TimerInfo(time)))
            .OnComplete(() => Debug.Log(""));

        //ステージ内のコインの総数と獲得したコインの数の割合でランク付け（A～C）
        int ratio = m_getCoinNum / m_totalCoinNum * 100;
        m_UISetActiveControl.DetermineTheRank(ratio);

        SaveScoreAndTime(m_currentScore, clearTime);
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
            SceneLoader.Instance.LoadSelectSceneWithTap();
        }
    }

    protected override void OnEnable()
    {
        m_eventSystemInGameScene.GetCoinEvent += GetCoin;
    }

    protected override void OnDisable()
    {
        m_eventSystemInGameScene.GetCoinEvent -= GetCoin;
    }
}
