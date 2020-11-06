using UnityEngine;

public class Seya : MonoBehaviour
{    // 瀬谷です
    [SerializeField] bool isLeft;
    private const float radius = 11.0f;
    private float angle = 0.0f;
    [SerializeField] float xSetPos;
    [SerializeField] float ySetPos;
    [SerializeField] float zSetPos;


    Vector3 objPos;

    void FixedUpdate()
    {


        if (isLeft)
        {
            objPos = new Vector3(radius * Mathf.Cos(angle) + xSetPos, ySetPos, zSetPos+ radius * Mathf.Sin(angle));
            angle += 2.0f * Mathf.PI / 50.0f;
        }
        else
        {
            objPos = new Vector3(radius * Mathf.Cos(angle) + xSetPos, ySetPos, zSetPos + radius * Mathf.Sin(angle));
            angle -= 2.0f * Mathf.PI / 50.0f;
        }
        transform.position = objPos;
    }
}