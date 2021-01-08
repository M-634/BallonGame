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
    private int m_currentScore = 0;
    [SerializeField] UISetActiveControl m_UISetActiveControl;
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
    /// 獲得スコアと残り時間を表示。それらを掛け合わせたトータルスコアを表示する。
    /// ハイスコアを更新したらセーブする
    /// </summary>
    public void Result(int leftTime)
    {
        //int totalScore = m_currentScore * leftTime;
       
        int score = 0;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(
        DOTween.To(() => score, num => score = num, m_currentScore, 2f)
            .OnUpdate(() => m_UISetActiveControl.GetResulScoreText.text = "Score: " + score.ToString())
            .OnComplete(() => Debug.Log("")));

        int time = 0;
        sequence.Append(
            DOTween.To(() => time, num => time = num, leftTime, 2f)
            .OnUpdate(() => m_UISetActiveControl.LeftTimeScoreText.text = "LeftTime;" + time.ToString())
            .OnComplete(() => Debug.Log("")));

        //int total = 0;
        //sequence.Append(
        //    DOTween.To(() => total, num => total = num, totalScore, 2f)
        //    .OnUpdate(() => m_UISetActiveControl.TotalScoreText.text = "TotalScore:" + total.ToString())
        //    .OnComplete(() =>
        //    {
        //        try
        //        {
        //            SaveAndLoad(totalScore, leftTime);//ここで意味不明なエラーが起きてる
        //        }
        //        catch (System.Exception e)
        //        {
        //            Debug.Log(e.Message);
        //        }
        //    }));

        //ステージ内のコインの総数と獲得したコインの数の割合でランク付け（S～C）
        int totalPoint = m_getCoinNum / m_totalCoinNum * 100;

        m_UISetActiveControl.TotalScoreText.text = DetermineTheRank(totalPoint);
        SaveAndLoad(totalPoint, leftTime);
    }

    private string DetermineTheRank(int raito)
    {
        if (raito > 90)
        {
            return "S";
        }
        else if (raito > 70)
        {
            return "A";
        }
        else if (raito > 50)
        {
            return "B";
        }
        else
        {
            return "C";
        } 
    }


    private void SaveAndLoad(int totalScore, int leftTime)
    {
        if (StageParent.Instance)
        {
            //ステージデータをセーブ
            StageParent.Instance.GetAppearanceStageData.Save(totalScore, leftTime);
            //ステージを非表示にする
            StageParent.Instance.GetAppearanceStagePrefab.SetActive(false);
            //ステージを初期化する
            StageParent.Instance.Initialization();
        }
        Debug.Log("aaa  ");
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
