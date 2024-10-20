using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPowerUp : PowerUpBase
{

    public override void Activate()
    {
        m_player.m_specialAttackCount++;
        base.Activate();
    }
}
