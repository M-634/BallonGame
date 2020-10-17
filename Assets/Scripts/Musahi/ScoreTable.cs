using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

[System.Serializable]
public class ScoreTable 
{
    public List<ScoreTableData> scoreTables;
}

[System.Serializable]
public class ScoreTableData
{
    public string stageName;
    public int highScore;
}
