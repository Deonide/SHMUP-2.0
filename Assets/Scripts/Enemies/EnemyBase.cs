using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected enum EnemyState
    {
        Spawning,
        Active
    }

    [SerializeField]
    protected int m_Health;

    [SerializeField]
    protected List<GameObject> PowerUps = new List<GameObject>();

    [SerializeField]
    protected GameObject m_explosion;

    protected EnemyState m_enemyState;

    protected Vector2 m_enemyPosition;
    protected Vector2 m_screenSpace;
    protected int m_enemyMoves;

    protected virtual void Start()
    {
        m_screenSpace = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        m_enemyMoves = Random.Range(1, 3);
    }

    #region Update
    protected virtual void Update()
    {
        m_enemyPosition = transform.position;

        switch (m_enemyState)
        {
            case EnemyState.Spawning:
                if (m_enemyPosition.y > 4)
                {
                    transform.position = Vector2.Lerp(transform.position, new Vector2(m_enemyPosition.x, m_enemyPosition.y - 0.5f), Time.deltaTime);
                }
                else
                {
                    m_enemyState = EnemyState.Active;
                }
                break;

        }
    }
    #endregion

    #region OnCollision
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            m_Health--;

            if(m_Health <= 0)
            {
                Instantiate(m_explosion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);

                Destroy(gameObject);
            }
        }
    }
    #endregion
}
