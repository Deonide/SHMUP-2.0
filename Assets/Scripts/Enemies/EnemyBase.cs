using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected int m_Health;

    [SerializeField]
    protected List<GameObject> PowerUps = new List<GameObject>(); 

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            m_Health--;

            if(m_Health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
