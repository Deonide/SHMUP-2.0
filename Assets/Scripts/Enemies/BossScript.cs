using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : ShootingTowardsPlayer
{
    private enum BossState
    {
        standstill,
        moveAround,
        circling
    }

    public float angularSpeed = 1f;
    public float circleRad = 1f;

    [SerializeField]
    private Vector2 fixedPoint;
    private float currentAngle;


    private BossState m_bossState;
    
    private int m_bossesSpawned;
    private int m_bossMoves;
    protected float m_timer;
    protected float m_enemySpeed;


    protected override void Start()
    {
        m_isBoss = true;
        m_bossMoves = Random.Range(1, 4);
        m_projectileCount++;
        m_bossesSpawned++;
        m_Health = m_bossesSpawned * 4 + 12;
        fixedPoint = transform.position;
        fixedPoint.y = 3;
        m_enemySpeed = 1 + Mathf.Round((GameManager.Instance.m_currentWave / 8));

        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (m_bossMoves == 1)
        {
            m_bossState = BossState.standstill;
        }
        else if(m_bossMoves == 2)
        {
            m_bossState = BossState.moveAround;
        }
        else
        {
            m_bossState= BossState.circling;
        }

        switch (m_bossState)
        {
            case BossState.standstill:
                if (m_Health > 0 && m_enemyState == EnemyState.Active)
                {
                    ;
                }

                break;
            case BossState.moveAround:
                if (m_Health > 0 && m_enemyState == EnemyState.Active)
                {
                    m_timer += Time.deltaTime;
                    transform.position = Vector2.Lerp(transform.position, new Vector2(Mathf.PingPong(m_timer * m_enemySpeed, m_screenSpace.x), m_enemyPosition.y), Time.deltaTime * m_enemySpeed);
                }
                break;
            case BossState.circling:
                if (m_Health > 0 && m_enemyState == EnemyState.Active)
                {
                    currentAngle += m_enemySpeed * Time.deltaTime;
                    Vector2 offset = new Vector2(Mathf.Sin(currentAngle), Mathf.Cos(currentAngle)) * circleRad;
                    transform.position = fixedPoint + offset;
                }
                break;

        }


    }
}
