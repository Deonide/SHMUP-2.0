using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    private int m_bulletSpeed = 10;
    private GameObject m_player;

    private Rigidbody2D m_rb;

    // Start is called before the first frame update
    void Start()
    {
        //Slaat de Rigidbody op in een variabele.
        m_rb = GetComponent<Rigidbody2D>();
        m_player = GameObject.FindGameObjectWithTag("Player");

        //Zorgt ervoor dat de bullet richting de speler gaat.
        Vector3 direction = m_player.transform.position - transform.position;
        m_rb.velocity = new Vector2(direction.x, direction.y).normalized * m_bulletSpeed;
        
        transform.up = direction;
        //Als de kogel niets raakt dan destroyed het zichzelf na 3 seconden.
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
