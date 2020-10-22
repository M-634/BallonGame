using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float forwardForce = 20;
    [SerializeField, Range(0, 1f)] public float brekeCoefficient = 0.995f;
    Rigidbody m_rb;
    public float speed;
    [SerializeField]public float horizontalSpeed = 2.0f;
    Vector3 mouthPosi;
    Vector3 mouthPrePosi;
    Touch touch;
    Vector2 touchPos = new Vector2();
    Vector2 touchTempPos;
    public float swipeDistance_x = 0;
    bool swipe = false;
    DebugUI debugUI = new DebugUI();
    [SerializeField] GameObject debugUIobj;
    [SerializeField] bool mouthDebug;


    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        debugUI = debugUIobj.GetComponent<DebugUI>();
    }
    // Update is called once per frame
    private void Update()
    {
        speed = m_rb.velocity.magnitude;
    }
    void FixedUpdate()
    {
        MoveForce();
        
        m_rb.velocity = m_rb.velocity * brekeCoefficient;
        if (m_rb.velocity.magnitude < 0.01)
        {
            m_rb.velocity = m_rb.velocity * 0;
        }

        if (mouthDebug)
        {
            MouthAim();
            MouthForce();
        }
        
        Swipe();
        RotatePlayer();

        
    }

    /// <summary>加減速を計算する</summary>
    void MoveForce()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                m_rb.AddForce(this.transform.forward * forwardForce);
            }
        }
    }

    void MouthForce()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_rb.AddForce(this.transform.forward * forwardForce);
        }
        else if (!Input.GetMouseButton(0))
        {
            //m_rb.velocity = m_rb.velocity * coefficient;
        }
    }

    void Swipe()
    {
        // 画面タッチが行われたら
        if (touch.phase == TouchPhase.Began)
        {
            touchTempPos = touch.position;
            swipeDistance_x = 0;
        }
        if (touch.phase == TouchPhase.Moved)
        {
            swipeDistance_x = touch.position.x - touchTempPos.x; //現状デバッグ用に変数作って取得してる
            touchPos = new Vector2(
            (swipeDistance_x) / Screen.width, 0);
            swipe = true;
        }
    }

    void MouthAim()
    {
        mouthPosi = Input.mousePosition;
        touchPos.x = mouthPosi.x / Screen.width;
    }
    void RotatePlayer()
    {
        float h = horizontalSpeed * touchPos.x;
        transform.Rotate(0, h, 0);
    }

    /// <summary>デバッグ用、SwipeCoefficientSliderのOnValueChangedで呼び出してる</summary>
    public void ChangeSwipeCoefficient()
    {
        horizontalSpeed = debugUI.swipeCoefficient.value;
    }

    /// <summary>これもデバッグ用、FowrardForceSliderのOnValueChangedで呼び出してる</summary>
    public void ChangeForwardForce()
    {
        forwardForce = debugUI.forwardForce.value;
    }

    /// <summary>これもデバッグ用、FowrardForceSliderのOnValueChangedで呼び出してる</summary>
    public void ChangeBrakeCoefficient()
    {
        brekeCoefficient = debugUI.brakeCoefficient.value;
    }
}
