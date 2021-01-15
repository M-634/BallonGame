using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cannon : MonoBehaviour
{
    [SerializeField] CannonBullet[] m_bullets;
    [SerializeField] Transform m_muzzlePos;
    [SerializeField] float m_shootPower;
    [SerializeField] float m_coolTime;
    [SerializeField] float m_bulletLifeTime;
    [SerializeField] string m_shootSe;
    int m_index = 0;

    private void OnEnable()
    {
        foreach (var item in m_bullets)
        {
            item.transform.position = m_muzzlePos.position;
            item.gameObject.SetActive(false);
        }
        StartCoroutine(CannonUpdate());
    }

    private void OnDisable()
    {
        StopCoroutine(CannonUpdate());
    }


    IEnumerator CannonUpdate()
    {
        while (true)
        {
            if (m_bullets[m_index].gameObject.activeSelf)
            {
                m_index++;
                if (m_index >= m_bullets.Length)
                {
                    m_index = 0;
                }
                continue;
            }
            m_bullets[m_index].transform.position = m_muzzlePos.position;
            m_bullets[m_index].gameObject.SetActive(true);
            yield return new WaitForSeconds(m_coolTime);
            m_bullets[m_index].Shoot(transform.forward * -1 * m_shootPower,m_bulletLifeTime);
            SoundManager.Instance.PlayGameSe(m_shootSe);
            m_index++;
            if (m_index >= m_bullets.Length)
            {
                m_index = 0;
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        var target = GameObject.FindGameObjectWithTag("CannonTarget").transform;
    //        var dir = target.position - m_muzzlePos.position;
    //        //plyaerの方へむく
    //        dir.y = 0;
    //        var lookRotation = Quaternion.LookRotation(dir * -1, Vector3.up);
    //        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 1f);
    //        //発射
    //        m_bullets[m_index].Shoot(dir.normalized * m_shootPower);
    //        SoundManager.Instance.PlayGameSe(m_shootSe);
    //    }
    //}
}
