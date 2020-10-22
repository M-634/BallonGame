using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private float m_speed = 5.0f;
    private Rigidbody r_body;
    private bool isMove = false;

    void Start()
    {
        r_body = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isMove)
        {
            r_body.velocity = new Vector3(0, 0, 2);

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                r_body.velocity = new Vector3(-1, 0, 2);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                r_body.velocity = new Vector3(1, 0, 2);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isMove = true;
    }
}
