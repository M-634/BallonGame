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
    [SerializeField] string m_shootSe;
    int m_index = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var target = GameObject.FindGameObjectWithTag("CannonTarget").transform;
            var dir = target.position - m_muzzlePos.position;
            //plyaerの方へむく
            dir.y = 0;
            var lookRotation = Quaternion.LookRotation(dir * -1, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 1f);
            //発射
            m_bullets[m_index].Shoot(dir.normalized * m_shootPower);
            SoundManager.Instance.PlayGameSe(m_shootSe);
        }
    }
}
