
///*********
/// memo : stageに持たせるべき情報
/// ハイスコア、クリア情報、天候情報、
///*********

using System;

[Serializable]
public class StageData
{
    public int HighScore;
    public bool IsStageClear;

    public enum WeatherConditons 
    {
        Initialize, Sunny, ThunderStorm, Hurricane
    }

}

