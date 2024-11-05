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
    protected int m_Score, m_moneyToGive;

    [SerializeField]
    protected List<GameObject> m_powerUps = new List<GameObject>();

    [SerializeField]
    protected GameObject m_explosion;

    [SerializeField]
    protected bool m_isBoss;

    private int m_powerUpDrop;
    private GameObject m_powerUpToDrop;

    private WaveManager m_waveManager;
    protected PlayerMovement m_player;
    protected EnemyState m_enemyState;

    protected Vector2 m_enemyPosition;
    protected Vector2 m_screenSpace;
    private   Vector2 m_spawnPosition;

    protected float m_Health = 3;

    protected virtual void Start()
    {
        m_screenSpace = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        m_waveManager = FindObjectOfType<WaveManager>();

        //Get the screen position of object in Pixels
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // Get the left and right side of the screen in world units
        float rightSideofScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        float leftSideOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)).x;

        float bottomSideOfTheScrean = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height / 2)).y;
        float topSideOfTheScrean = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height - 4)).y;

        m_spawnPosition.x = Random.Range(leftSideOfScreenInWorld, rightSideofScreenInWorld - 1);
        m_spawnPosition.y = Random.Range(bottomSideOfTheScrean, topSideOfTheScrean -1);
        m_player = FindObjectOfType<PlayerMovement>();
    }

    protected virtual void Update()
    {
        m_enemyPosition = transform.position;

        switch (m_enemyState)
        {
            case EnemyState.Spawning:
                if (!m_isBoss)
                {
                    if (m_enemyPosition != m_spawnPosition)
                    {
                        float distance = Vector2.Distance(m_enemyPosition, m_spawnPosition);

                        float lerpfactor = Mathf.Clamp01(Time.deltaTime * 3 * distance);

                        transform.position = Vector2.Lerp(transform.position, m_spawnPosition, lerpfactor);

                        if (distance <= 0.5)
                        {
                            m_enemyState = EnemyState.Active;
                        }
                    }
                }

                else
                {
                    if(transform.position.y > 3.5)
                    {
                        transform.position = Vector2.Lerp(transform.position, new Vector2(m_enemyPosition.x, m_enemyPosition.y - 0.5f), Time.deltaTime);
                    }
                    else
                    {
                        m_enemyState = EnemyState.Active;
                    }
                }    
                break;
        }

        if(m_enemyPosition.y <= -6)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_enemyState == EnemyState.Active)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                m_Health -= m_player.m_bulletDamage;

                if (m_Health <= 0)
                {
                    m_player.AddScore(m_Score);
                    m_player.GetMoney(m_moneyToGive);
                    Instantiate(m_explosion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    PowerUpDrops();
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (m_isBoss)
        {
            m_waveManager.RemoveBossFromList(this.gameObject);
        }
        m_waveManager.RemoveFromList(this.gameObject);
    }

    private void PowerUpDrops()
    {
        if (m_isBoss && !m_player.m_bulletUltra)
        {
            m_powerUpDrop = 0;
        }
        else
        {
            m_powerUpDrop = Random.Range(0, m_powerUps.Count);
            if(m_powerUpDrop == 0 && !m_isBoss || m_powerUpDrop == 0 && m_player.m_bulletUltra)
            {
                m_powerUpDrop++;
            }
        }
        m_powerUpToDrop = m_powerUps[m_powerUpDrop];
        Instantiate(m_powerUpToDrop, transform.position, Quaternion.identity);
    }
}
