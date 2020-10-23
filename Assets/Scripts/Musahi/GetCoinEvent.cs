using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// コイン獲得時のイベント
/// </summary>
public class GetCoinEvent :  CollisionEvent<GetCoinEvent>
{
    public override void AddEvent()
    {
        ScoreManager.Instance.GetCoin();
        this.gameObject.SetActive(false);
        Debug.Log("get coin");
    }
}
