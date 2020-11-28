using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatStickCameraWork : MonoBehaviour
{
    /// <summary>
    /// カメラの動きとなる元のスクリプト
    /// </summary>
    [SerializeField] GameObject floatingJoystickobj;
    FloatingJoystick floatingJoystick;

    /// <summary>横軸の回転係数 </summary>
    [Header("横の回転の速さ")]
    [SerializeField] float horizontalCoefficient = 0.5f;
    /// <summary>縦軸の回転係数 </summary>
    [Header("縦の回転の速さ")]
    [SerializeField] float verticalCoefficient = 0.5f;

    Vector2 centerPosition;
    /// <summary>
    /// カメラの動く範囲の円の半径
    /// </summary>
    [Header("カメラの動く円の範囲")]
    [SerializeField] float radius = 10;

    private void Start()
    {
        floatingJoystick = floatingJoystickobj.GetComponent<FloatingJoystick>();
        centerPosition = transform.localPosition;
    }

    private void FixedUpdate()
    {
        SetCameraTargetPosi();
    }


    /// <summary>
    /// カメラの向きの制御
    /// </summary>
    public void SetCameraTargetPosi()
    {
        Vector2 myPosi = transform.localPosition;
        
        if (Vector2.Distance(myPosi, centerPosition) < radius)
        {
            transform.Translate(floatingJoystick.Horizontal * horizontalCoefficient
                , floatingJoystick.Vertical * verticalCoefficient, 0);
        }
        else
        {
            Vector2 returnForce = (centerPosition - myPosi) * Time.deltaTime ;
            transform.Translate(returnForce);
        }
    }
}
