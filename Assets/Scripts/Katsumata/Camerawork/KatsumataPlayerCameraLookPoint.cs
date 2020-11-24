using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class KatsumataPlayerCameraLookPoint : MonoBehaviour
{
    int m_layerMask;
    [Header("カメラの振り向く速さ")]
    /// <summary>カメラがCameraAimPoint(このオブジェクト)を見る速さ </summary>
    [SerializeField] float aimSpeed = 1.0f;
    [SerializeField] GameObject onObject;
    Vector3 v_lastHitPosi;
    [SerializeField] GameObject g_lastHitPosi;
    bool existlastHitPosi = false;
    private void Start()
    {
        int layer = LayerMask.NameToLayer("Drag");
        m_layerMask = 1 << layer;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        float max_distance = 100f;

        bool is_hit = Physics.Raycast(ray, out hitInfo, max_distance, m_layerMask);
        if (is_hit)
        {
            //g_lastHitPosi.SetActive(false);

            transform.position = Vector3.Lerp(transform.position, hitInfo.point, Time.deltaTime * aimSpeed);
            g_lastHitPosi.transform.position = hitInfo.point;
            v_lastHitPosi = hitInfo.point;
        }
        else
        {
            //g_lastHitPosi.SetActive(true);
            transform.position = Vector3.Lerp(transform.position, g_lastHitPosi.transform.position, Time.deltaTime * aimSpeed);

            //if (!existlastHitPosi)
            //{
                
            //    existlastHitPosi = true;
            //}
            
        }
    }
}
