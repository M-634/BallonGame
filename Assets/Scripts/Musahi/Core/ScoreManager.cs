using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Collections.LowLevel.Unsafe;

/// <summary>
///ゲーム中のスコア周りを管理するクラス
/// </summary>
[RequireComponent(typeof(UISetActiveControl))]
public class ScoreManager : Reciver
{
    private int m_highScore;
    private int m_currentScore;
    /// <summary>1コイン獲得時に得られるスコア</summary>
    [SerializeField] int m_getCoinScore = 100;

    UISetActiveControl m_UISetActiveControl;
    SaveAndLoadWithJSON m_json;
    string m_path;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        m_UISetActiveControl = GetComponent<UISetActiveControl>();
        m_UISetActiveControl.CurrentScoreText.text = "Score: ";
        m_json = new SaveAndLoadWithJSON();
        m_highScore = m_json.LoadHighScore();
        Debug.Log("HighScore: " + m_highScore);
    }

    /// <summary>
    /// プレイヤーがコインに衝突したら呼ばれる関数
    /// </summary>
    public void GetCoin()
    {
        //2回目でエラーが起きる原因不明
        m_currentScore += m_getCoinScore;
        m_UISetActiveControl.CurrentScoreText.text = "Score: " + m_currentScore;
    }


    /// <summary>
    /// ゲームクリア!
    /// 獲得スコアと残り時間を表示。それらを掛け合わせたトータルスコアを表示する。
    /// ハイスコアを更新したらセーブする
    /// </summary>
    public void Result(int leftTime)
    {
        int totalScore = m_currentScore * leftTime;

        int score = 0;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(
        DOTween.To(() => score, num => score = num, m_currentScore, 2f)
            .OnUpdate(() => m_UISetActiveControl.GetScoreText.text = ("Score: " + score.ToString()))
            .OnComplete(() => Debug.Log("")));

        //ここ修正ポイント
        int time = 0;
        sequence.Append(
            DOTween.To(() => time, num => time = num, leftTime, 2f)
            .OnUpdate(() => m_UISetActiveControl.LeftTimeScoreText.text = ("LeftTime;" + time.ToString()))
            .OnComplete(() => Debug.Log("")));


        int total = 0;
        sequence.Append(
            DOTween.To(() => total, num => total = num, totalScore, 2f))
            .OnUpdate(() => m_UISetActiveControl.TotalScoreText.text = ("TotalScore:" + total.ToString()))
            .OnComplete(() => SaveHighScore(totalScore));
    }

    private void SaveHighScore(int totalScore)
    {
        if (totalScore > m_highScore)
        {
            // m_json.SaveHighScore(totalScore);
        }

        UnSubscribe();
        //ステージを非表示にする
        if (StageParent.Instance != null)
        {
            StageParent.Instance.GetAppearanceStage.SetActive(false);
            //ステージを初期化する
            StageParent.Instance.Initialization();
        }
        //タップしたらセレクト画面に戻る(タップしてください。みたいなテキストを出す)
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadWithTap("SelectScene 1");
        }
    }

    protected override void Subscribe()
    {
        Sender.GetCoinEvent += () => GetCoin();
        base.Subscribe();
    }

    protected override void UnSubscribe()
    {
        Sender.GetCoinEvent -= () => GetCoin();
        base.UnSubscribe();
    }
}
