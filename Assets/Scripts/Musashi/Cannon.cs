using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cannon : MonoBehaviour
{
    [SerializeField] CannonBullet m_bullet;
    [SerializeField] Transform m_muzzlePos;
    [SerializeField] GameSePlayer m_gameSePlayer;
    [SerializeField] float m_shootPower;
    [SerializeField] float m_startWaitTime;
    [SerializeField] float m_coolTime;
    [SerializeField] float m_bulletLifeTime;
    [SerializeField] string m_shootSe;

    private void OnEnable()
    {
        StartCoroutine(CannonUpdate());
    }

    private void OnDisable()
    {
        StopCoroutine(CannonUpdate());
    }


    IEnumerator CannonUpdate()
    {
        float r = Random.Range(0.1f, 1f);
        yield return new WaitForSeconds(m_startWaitTime + r);//ゲーム開始から何秒か経ってから起動する
        while (true)
        {
            yield return new WaitForSeconds(m_coolTime);
            m_bullet.Shoot(m_muzzlePos.forward * m_shootPower,m_bulletLifeTime);
            m_gameSePlayer.PlayFirstAudioClip(0.3f);
            yield return new WaitForSeconds(m_bulletLifeTime);
            m_bullet.transform.position = m_muzzlePos.position;
        }
    }
}
