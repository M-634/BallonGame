using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;


/// <summary>
///ゲーム中のスコア周りを管理するクラス
/// </summary>
public class ScoreManager : MonoBehaviour
{
    [SerializeField] int m_highScore;
    [SerializeField] int m_currentScore;
    [SerializeField] Text m_currentScoreText;
    /// <summary>1コイン獲得時に得られるスコア</summary>
    [SerializeField] int m_getCoinScore = 100;

    //リザルトシーンは作らないかな。
    [Header("リザルトテキスト")]
    [SerializeField] Text m_leftTimeText;
    [SerializeField] Text m_getScoreText;
    [SerializeField] Text m_totalScoreText; 

    SaveAndLoadWithJSON m_json;
    string m_path;

    // Start is called before the first frame update
    void Start()
    {
        //ここも変更Point！現状はシーンの名前でパスを振り分けているが、1つのシーンを使い回すため
        //stageごとにIDなどを振り分ける
        //ハイスコアをロードする。
        //#if UNITY_ANDROID
        //        m_path = Application.streamingAssetsPath + $"/{SceneManager.GetActiveScene().name}_HighScoreData.json";  
        //#else
        //        m_path = Application.dataPath + $"/{SceneManager.GetActiveScene().name}_HighScoreData.json";
        //#endif
        m_path = StageParent.Instance.FullPath;
        m_json = new SaveAndLoadWithJSON(m_path);
        m_highScore = m_json.LoadHighScore();
    }

    /// <summary>
    /// プレイヤーがコインに衝突したら呼ばれる関数
    /// </summary>
    public void GetCoin()
    {
        m_currentScore += m_getCoinScore;
        m_currentScoreText.text = "Score: " + m_currentScore.ToString();
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
            .OnUpdate(() => m_getScoreText.text = ("Score: " + score.ToString()))
            .OnComplete(() => Debug.Log("")));

        //ここ修正ポイント
        int time = 0;
        sequence.Append(
            DOTween.To(() => time, num => time = num, leftTime, 2f)
            .OnUpdate(() => m_leftTimeText.text = ("LeftTime;" + time.ToString()))
            .OnComplete(() => Debug.Log("")));


        int total = 0;
        sequence.Append(
            DOTween.To(() => total, num => total = num,totalScore , 2f))
            .OnUpdate(() => m_totalScoreText.text = ("TotalScore:" + total.ToString()))
            .OnComplete(() => SaveHighScore(totalScore));
    }

    private void SaveHighScore(int totalScore)
    {
        if (totalScore > m_highScore)
        {
            m_json.SaveHighScore(totalScore);
        }
    }
}
