using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


/// <summary>
/// シングルトンパターンを実装する抽象クラス
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingletonMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_instance;
    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                Type t = typeof(T);
                m_instance = (T)FindObjectOfType(t);
                if (m_instance == null)
                {
                    Debug.LogWarning(t + "をアタッチしているGameObjectはありません");
                    return null;
                }
            }
            return m_instance;
        }
    }

    virtual protected void Awake()
    {
        if (this != Instance)//ここの条件式を変えたらエラーが治った！
        {
            Debug.LogWarning(typeof(T) + "は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました"
                + "アタッチされているGameObjectは" + Instance.gameObject.name + "です");
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);は継承先で書くこと!
    }

}
