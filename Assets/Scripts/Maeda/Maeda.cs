using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Maeda : MonoBehaviour
{
    [SerializeField] bool isDrill;
    [SerializeField] bool isLeft;

    private float increase = 1;
    private const float radius = 11.0f;
    private float angle = 0.0f;
    [SerializeField] float xDefaultPos;

    Vector3 objPos;

    private void FixedUpdate()
    {
        if (!isDrill)
        {
            if (isLeft)
            {
                objPos = new Vector3(radius * Mathf.Cos(angle) + xDefaultPos, 1, 130 + radius * Mathf.Sin(angle));
                angle += 2.0f * Mathf.PI / 50.0f;
            }
            else
            {
                objPos = new Vector3(radius * Mathf.Cos(angle) + xDefaultPos, 1, 130 + radius * Mathf.Sin(angle));
                angle -= 2.0f * Mathf.PI / 50.0f;
            }
            transform.position = objPos;
        }
        else
        {
            gameObject.transform.localScale += new Vector3(0, 10 * increase, 0) * Time.deltaTime;
            if (gameObject.transform.localScale.y >= 20)
            {
                increase *= -1;
            }
            else if (gameObject.transform.localScale.y < 0)
            {
                increase *= -1;
            }
        }
    }
}