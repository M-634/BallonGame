using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全ての障害物にアタッチするクラス
/// </summary>
public class Obstacle : CollisionEvent<Obstacle>
{
    public override void AddEvent()
    {
        TimeScheduler.Instance.GameOver();
    }
}
