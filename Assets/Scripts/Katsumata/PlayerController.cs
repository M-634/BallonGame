using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float forwardForce = 1;
    [SerializeField, Range(0, 1f)] float coefficient = 0.95f;
    Rigidbody m_rb;
    [SerializeField] float horizontalSpeed = 2.0f;
    Touch touch;
    Vector2 touchPos = new Vector2();
    Vector2 touchTempPos;


    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                m_rb.AddForce(this.transform.forward * forwardForce);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                m_rb.velocity = m_rb.velocity * coefficient;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            m_rb.AddForce(this.transform.forward * forwardForce);
        }
        else if (!Input.GetMouseButton(0))
        {
            m_rb.velocity = m_rb.velocity * coefficient;
        }

        if (m_rb.velocity.magnitude < 0.01)
        {
            m_rb.velocity = m_rb.velocity * 0;
        }
        //MouthSwipe();
        Swipe();
        float h = horizontalSpeed * touchPos.x;

        transform.Rotate(0, h, 0);
        Debug.Log(m_rb.velocity.magnitude);


    }

    void Swipe()
    {
        // 画面タッチが行われたら
        if (touch.phase == TouchPhase.Began)
        {
            touchTempPos = touch.position;
        }
        if (touch.phase == TouchPhase.Moved)
        {
            touchPos = new Vector2(
            (touch.position.x - touchTempPos.x) / Screen.width, 0);
        }
    }
}
