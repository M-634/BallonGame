using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CannonBullet : MonoBehaviour
{
    Rigidbody m_rb;
    Vector3 m_firstpos;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_firstpos = transform.position;
    }

    public void Shoot(Vector3 dir)
    {
        gameObject.SetActive(true);
        m_rb.AddForce(dir);
        StartCoroutine(gameObject.SetActive(false,10f));
    }

    public void OnDisable()
    {
        transform.position = m_firstpos; 
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
