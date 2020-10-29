using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全ての障害物のベースクラス.
/// プレイヤーに当たるとゲームオーバーする
/// </summary>
public class Obstacle : Sender
{
   
    /// <summary>ゲームオーバー </summary>
    public override void CollisionEvent()
    {
        ExecuteGameOverEvent();
        Debug.Log("GameOver!");
    }
}

