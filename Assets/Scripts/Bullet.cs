using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int m_bulletSpeed = 10;

    private Rigidbody2D m_rb;

    void Start()
    {
        //Slaat de Rigidbody op in een variabele.
        m_rb = GetComponent<Rigidbody2D>();

        //Schiet de kogel af op een bepaalde snelheid.
        m_rb.velocity = transform.up * m_bulletSpeed;

        //Als de kogel niets raakt dan destroyed het zichzelf na 3 seconden.
        Destroy(gameObject, 3f);
    }

    //Als de gameObject iets raakt gaat ie stuk.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Voordat de bullet gedestroyed word spawned er eerst een particle effect in. (m_explosionPrefab)
/*        Instantiate(m_explosionPrefab, transform.position, Quaternion.identity);*/
        Destroy(this.gameObject);
    }

}
