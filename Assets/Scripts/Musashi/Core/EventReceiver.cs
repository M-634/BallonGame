using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームシーン上で起こるイベントをまとめるイベントシステムを
/// 購読する抽象クラス
/// </summary>
public abstract class EventReceiver<T> : MonoBehaviour where T :MonoBehaviour
{
    protected EventSystemInGameScene m_eventSystemInGameScene;
    /// <summary>勝又用：Playerの動きをテストしたいときはチェックしてね</summary>
    public  bool m_doDebug;

    private void Awake()
    {
        if (m_doDebug) return;
        m_eventSystemInGameScene = GameObject.FindGameObjectWithTag("StageManager").GetComponent<EventSystemInGameScene>();
    }

    //イベントを登録
    protected abstract void OnEnable();
    //イベント解除
    protected abstract void OnDisable();
}
