using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MovingEnemyBase : EnemyBase
{
    protected float m_timer;
    protected float m_enemySpeed;

    protected override void Start()
    {
        m_enemySpeed = 1 + Mathf.Round((GameManager.Instance.m_currentWave / 8));
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (m_Health > 0 && m_enemyState == EnemyState.Active)
        {
            m_timer += Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, new Vector2(Mathf.PingPong(m_timer * m_enemySpeed, m_screenSpace.x), m_enemyPosition.y), Time.deltaTime * m_enemySpeed);
        }
    }
}
