using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全ての障害物のベースクラス.
/// プレイヤーに当たるとゲームオーバーする
/// </summary>
public class Obstacle : MonoBehaviour,IEventCollision
{
    /// <summary>ゲームオーバー </summary>
    public void CollisionEvent(EventSystemInGameScene eventSystem)
    {
        eventSystem.ExecuteGameOverEvent();
        Debug.Log("GameOver!");
    }
}

