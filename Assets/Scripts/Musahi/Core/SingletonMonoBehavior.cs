using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// シングルトンパターンを実装する抽象クラス
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMonoBehavior<T> : MonoBehaviour where T:MonoBehaviour
{
    [SerializeField] protected bool m_dontDestroyOnLoad = false;
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
                    Debug.LogError(t + "をアタッチしているGameObjectはありません");
                    return null;
                }
            }
            return m_instance;
        }
    }

    virtual protected void Awake()
    {
        if (m_instance != null && this != m_instance)
        {
            Debug.LogWarning(typeof(T) + "は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました"
                + "アタッチされているGameObjectは" + Instance.gameObject.name + "です");
            Destroy(this.gameObject);
            return;
        }

        if (m_dontDestroyOnLoad)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

}
