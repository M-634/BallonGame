using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using System.Linq;

public class MaedaPendulum : MonoBehaviour
{
    [SerializeField] GameObject pivot;
    [Tooltip("0∼0.5の間で")]
    [SerializeField] float m_rad;
    [Tooltip("4以上で")]
    [SerializeField] int pathVertex;
    [Tooltip("往復にかかる秒数")]
    [SerializeField] float interval;
    [Tooltip("X座標の振幅")]
    [SerializeField] float x_amplitude;
    [Tooltip("Y座標の振幅")]
    [SerializeField] float y_amplitude;

    private void Awake()
    {
        float rad = (2 * m_rad) / pathVertex;
        float radNatureNum = 1.5f - m_rad;
        List<Vector3> vector3s = new List<Vector3>();
        List<Vector3> aftervec = new List<Vector3>();
        for (int i = 0; i < pathVertex; i++)
        {
            float radius = Mathf.PI * radNatureNum;
            Vector3 vertex = new Vector3(Mathf.Cos(radius) * x_amplitude + pivot.transform.position.x, Mathf.Sin(radius) * y_amplitude + pivot.transform.position.y, pivot.transform.position.z);
            vector3s.Add(vertex);
            radNatureNum += rad;
        }
        for (int i = vector3s.Count - 1; i > 0; i--)
        {
            aftervec.Add(vector3s[i]);
        }
        vector3s.RemoveAt(0);
        Vector3[] beforeArray = vector3s.ToArray();
        Vector3[] afterArray = aftervec.ToArray();
        DOTween.Sequence()
            .Append(transform.DOMove(vector3s[0], 0))
            .Append(transform.DOPath(beforeArray, interval, PathType.CatmullRom).SetEase(Ease.InOutCubic))
            .Append(transform.DOPath(afterArray, interval, PathType.CatmullRom).SetEase(Ease.InOutCubic))
            .SetLoops(-1)
            .Play();
    }
}
