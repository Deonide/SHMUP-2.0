using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MovingEnemyBase 
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
            m_timer += Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, new Vector2(Mathf.PingPong(m_timer * m_enemySpeed, m_screenSpace.x), m_enemyPosition.y), Time.deltaTime * m_enemySpeed);

            if (m_enemyMoves == 2)
            {
                transform.Translate(0, enemyPositionY, 0);
            }
        }
    }
}
