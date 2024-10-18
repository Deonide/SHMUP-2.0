using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : PowerUpBase
{
    public override void Activate()
    {
        m_player.m_Health = 3;

        m_player.m_health2.SetActive(true);
        m_player.m_health3.SetActive(true);
    }
}
