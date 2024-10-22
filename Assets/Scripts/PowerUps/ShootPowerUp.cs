using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPowerUp : PowerUpBase
{
    public override void Activate()
    {
        if (m_player.m_bulletPowered != true) 
        { 
            m_player.m_bulletPowered = true;
        }
      
        else if (m_player.m_bulletUltra != true && m_player.m_bulletPowered == true)
        {
            m_player.m_bulletUltra = true;
            m_player.m_bulletPowered = false;
        }

        base.Activate();
    }
}
