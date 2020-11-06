using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class MaedaPendulum : MonoBehaviour
{
    [SerializeField] GameObject pivot;
    private float radius = 1.5f;
    private bool isPlus;
    [SerializeField] float speed;
    [SerializeField] float interval;
    [Tooltip("X座標の振幅")]
    [SerializeField] float x_amplitude;
    [Tooltip("Y座標の振幅")]
    [SerializeField] float y_amplitude;

    private void Start()
    {
        DOTween.Sequence()
            .Join(transform.DOMove(GetPendulumTransform(), 0))
            .Append(transform.DOMove(GetPendulumTransform(), interval))
            .Append(transform.DOMove(GetPendulumTransform(), interval))
            .Append(transform.DOMove(GetPendulumTransform(), interval))
            .Append(transform.DOMove(GetPendulumTransform(), interval))
            .Append(transform.DOMove(GetPendulumTransform(), interval))
            .Append(transform.DOMove(GetPendulumTransform(), interval))
            .Append(transform.DOMove(GetPendulumTransform(), interval))
            .Append(transform.DOMove(GetPendulumTransform(), interval))
            .Append(transform.DOMove(GetPendulumTransform(), interval))
            .Append(transform.DOMove(GetPendulumTransform(), interval))
            .Append(transform.DOMove(GetPendulumTransform(), interval))
            .Append(transform.DOMove(GetPendulumTransform(), interval))
            .SetLoops(-1)
            .Play();
    }

    Vector3 GetPendulumTransform()
    {
        Vector3 vector = new Vector3(pivot.transform.position.x + Mathf.Cos(Mathf.PI * radius) * x_amplitude, pivot.transform.position.y + Mathf.Sin(Mathf.PI * radius) * y_amplitude, pivot.transform.position.z);
        if (isPlus)
        {
            radius += speed;
            if (radius > 1.75f)
            {
                isPlus = false;
            }
        }
        else
        {
            radius -= speed;
            if (radius < 1.25f)
            {
                isPlus = true;
            }
        }
        return vector;
    }
}
