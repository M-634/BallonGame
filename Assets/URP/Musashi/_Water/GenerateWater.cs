using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoGenerateWater : MonoBehaviour
{
    [SerializeField] GameObject[] m_tiles = default;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var tile in m_tiles)
        {
            Plop(tile.transform, size: 1, bottom: 5);
        }
    }

    private static void Plop(Transform t, float extraDelay = 0, float size = 1,float bottom = 2)
    {
        Vector3 pos = t.position;
        Vector3 rot = t.eulerAngles;
        const float duration = 1.5f;
        float delay = GetDelay(t) + extraDelay;
        t.DOScale(new Vector3(size, 1, size), duration).From(Vector3.zero).SetDelay(delay);
        t.DOLocalRotate(rot, duration).From(new Vector3(0, rot.y + 60, 0)).SetDelay(delay);
        t.DOMoveY(pos.y, duration).From(pos.y - bottom).SetDelay(delay);
    }

    private static float GetDelay(Transform t)
    {
        Vector3 pos = t.position;
        return pos.magnitude / 50 + pos.y / 2;
    }
}
