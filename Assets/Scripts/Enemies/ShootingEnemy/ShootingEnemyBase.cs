using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingEnemyBase : EnemyBase
{
    [SerializeField]
    protected GameObject m_player;

    [SerializeField]
    private GameObject m_bulletPrefab;

    private float m_projectileCount;
    protected float m_attackTimer;
    protected float m_attackInterval;
    protected float minAttackTime = 1;
    protected float maxAttackTime = 5;

    protected override void Start()
    {
        m_attackInterval = 3;

        m_projectileCount = 1 + Mathf.Round(GameManager.Instance.m_currentWave / 2f);
        base.Start();
    }

    #region update
    protected override void Update()
    {
        base.Update();

        if (m_Health > 0 && m_enemyState == EnemyState.Active)
        {
            if (m_enemyMoves == 2 && m_player != null)
            {
                transform.up = transform.position - m_player.transform.position;
            }
            //Telt bij de attackTimer de deltatijd op.
            m_attackTimer += Time.deltaTime;

            //Als (de attackTimer groter of gelijk is aan de attackInterval)
            if (m_attackTimer >= m_attackInterval)
            {
                //Roept de Shoot() functie aan
                Shoot();

                //Zet de attackTimer op 0.
                m_attackTimer = 0;
            }
        }
    }
    #endregion

    public void Shoot()
    {
        //Instantiate de bulletprefab op de plek van de vijand en de rotatie van de vijand.
        Instantiate(m_bulletPrefab, transform.position, transform.rotation);

        //Berekent de nieuwe attackInterval met de Random class
        m_attackInterval = Random.Range(minAttackTime, maxAttackTime);
    }

}
