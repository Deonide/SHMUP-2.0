using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDown : ShootingEnemyBase
{
    protected override void Start()
    {
        m_attackDirection = Vector2.down;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
