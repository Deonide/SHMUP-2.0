using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int m_bulletSpeed = 10;



    private Rigidbody2D m_rb;
    private PlayerMovement m_player;

    void Start()
    {
        //Slaat de Rigidbody op in een variabele.
        m_rb = GetComponent<Rigidbody2D>();

        //Schiet de kogel af op een bepaalde snelheid.
        m_rb.velocity = transform.up * m_bulletSpeed;

        //Als de kogel niets raakt dan destroyed het zichzelf na 3 seconden.
        Destroy(gameObject, 3f);

        m_player = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (m_player.m_bulletPowered != true)
        {
            m_player.m_bulletDamage = 1;
        }
        else if (m_player.m_bulletPowered == true)
        {
            m_player.m_bulletDamage = 2;
        }
        else if (m_player.m_bulletUltra != true)
        {
            m_player.m_bulletDamage = 3;
        }
    }

    //Als de gameObject iets raakt gaat ie stuk.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
