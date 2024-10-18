using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTowardsPlayer : ShootingEnemyBase
{ 
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(m_player != null && m_enemyState == EnemyState.Active)
        {
            transform.up = transform.position - m_player.transform.position;
            m_attackDirection = m_player.transform.position - transform.position;
        }
    }
}
