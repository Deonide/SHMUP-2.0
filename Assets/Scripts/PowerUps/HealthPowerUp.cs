using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : PowerUpBase
{
    public override void Activate()
    {
        m_player.AddHealth();
        base.Activate();
    }
}
