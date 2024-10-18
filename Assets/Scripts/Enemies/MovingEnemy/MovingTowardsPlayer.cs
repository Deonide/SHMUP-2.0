using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTowardsPlayer : MovingEnemyBase
{ 
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        float enemyPositionY = -m_enemySpeed * Time.deltaTime / 10;
        if (m_Health > 0 && m_enemyState == EnemyState.Active)
        {
            transform.Translate(0, enemyPositionY, 0);
        }
    }
}
