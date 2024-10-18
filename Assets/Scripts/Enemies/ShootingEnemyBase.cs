using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingEnemyBase : EnemyBase
{


    [SerializeField]
    protected GameObject m_bulletPrefab;

    [SerializeField]
    protected GameObject m_bulletSpawnPoint;

    protected float m_attackInterval;
    private float m_maxAttackInterval = 2f;
    protected float m_bulletSpeed = 10;
    protected float m_projectileCount;

    protected Vector2 m_attackDirection;

    protected override void Start()
    {
        m_projectileCount = 1 + Mathf.Round(GameManager.Instance.m_currentWave / 4f);
        if (m_projectileCount > 3)
        {
            m_projectileCount = 3;
        }
        m_attackInterval = m_maxAttackInterval;
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if(m_enemyState == EnemyState.Active)
        {
            m_attackInterval -= Time.deltaTime;
            StartCoroutine(Shoot());
        }
    }


    private IEnumerator Shoot()
    {
        if(m_attackInterval <= 0)
        {
            for(int i = 0; i < m_projectileCount; i++)
            {
                //Instantiate de bulletprefab op de plek van de vijand en de rotatie van de vijand.
                GameObject bulletSpawn = Instantiate(m_bulletPrefab, m_bulletSpawnPoint.transform.position, m_bulletSpawnPoint.transform.rotation);

                bulletSpawn.GetComponent<Rigidbody2D>().velocity = new Vector2(m_attackDirection.x, m_attackDirection.y).normalized * m_bulletSpeed;

                m_attackInterval = m_maxAttackInterval;

                yield return new WaitForSeconds(0.2f);
            }
        }
        yield return null;
    }


}
