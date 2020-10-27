using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
    [SerializeField] Text m_resultScoreText;
    [SerializeField] Text m_totalScoreText; 

    SaveAndLoadWithJSON m_json;
    string m_path;

    // Start is called before the first frame update
    void Start()
    {
        //ハイスコアをロードする。
#if UNITY_ANDROID
        m_path = Application.streamingAssetsPath + $"/{SceneManager.GetActiveScene().name}_HighScoreData.json";  
#else
        m_path = Application.dataPath + $"/{SceneManager.GetActiveScene().name}_HighScoreData.json";
#endif
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
    public void Result(float leftTime)
    {
  
        if (m_currentScore > m_highScore)
        {
            //リザルトをセーブ
            m_json.SaveHighScore(m_currentScore);
            m_highScore = m_currentScore;
        }
    }
}
