using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float forwardForce = 1;
    [SerializeField, Range(0, 1f)] float coefficient = 0.95f;
    Rigidbody m_rb;
    [SerializeField] float horizontalSpeed = 2.0f;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_rb.AddForce(this.transform.forward * forwardForce);
        }
        if (!Input.GetMouseButton(0))
        {
            m_rb.velocity = m_rb.velocity * coefficient;
        }

        float h = horizontalSpeed * Input.GetAxis("Mouse X");

        transform.Rotate(0, h, 0);
        Debug.Log(m_rb.velocity.magnitude);


    }
}
