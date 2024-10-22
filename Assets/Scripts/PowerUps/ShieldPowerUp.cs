using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : PowerUpBase
{
    public override void Activate()
    {
        m_player.AddShield();
        base.Activate();
    }
}
