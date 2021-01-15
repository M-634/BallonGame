using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CannonBullet : MonoBehaviour
{
    Rigidbody m_rb;
    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    public void Shoot(Vector3 dir,float lifeTime)
    {
        gameObject.SetActive(true);
        m_rb.AddForce(dir);
        StartCoroutine(gameObject.SetActive(false,lifeTime));
    }
}

public static class ExtendGameObjectClass
{
    public static IEnumerator SetActive(this GameObject gameObject,bool value,float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        gameObject.SetActive(value);
    }
}
