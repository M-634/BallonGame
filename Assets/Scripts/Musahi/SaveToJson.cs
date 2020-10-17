using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///Json形式でセーブする。ステージのid（名前か？）と
///ステージごとのハイスコアをセーブする
/// </summary>
public class SaveToJson 
{
   public static void SaveHighScore(int id,int score)
    {
        var data = new SaveData(id,score);
      
    }
    
    public static void LoadHighScore()
    {

    }
}

[System.Serializable]
public class SaveData
{
    public int ID;
    public int HighScore;

    public  SaveData(int id ,int highScore)
    {
        ID = id;
        HighScore = highScore;
    }
}


