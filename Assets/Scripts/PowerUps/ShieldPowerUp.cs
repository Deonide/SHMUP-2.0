using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : PowerUpBase
{
    public override void Activate()
    {
        m_player.m_shielded = true;
        m_player.m_shieldsAmount = 3;
        m_player.m_shield.SetActive(true); 
        m_player.m_shield2.SetActive(true);
        m_player.m_shield3.SetActive(true);
        base.Activate();
    }
}
