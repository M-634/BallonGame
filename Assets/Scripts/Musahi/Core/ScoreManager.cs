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
    [SerializeField] int m_currentScore;
    [SerializeField] int m_highScore;
    /// <summary>1コイン獲得時に得られるスコア</summary>
    [SerializeField] int m_getCoinScore = 100;

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
    }

    /// <summary>
    /// タイムリミットの残り時間をスコアに加えへリザルトを出す。
    /// </summary>
    public void AddTimeScore(int timeScore)
    {
        m_currentScore += timeScore;
        Result();
    }

    private void Result()
    {
        if (m_currentScore > m_highScore)
        {
            //リザルトをセーブ
            m_json.SaveHighScore(m_currentScore);
            m_highScore = m_currentScore;
        }

        Debug.Log("Score: " + m_currentScore);
        Debug.Log("HighScore: " + m_highScore);
    }
}
