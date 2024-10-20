using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : PowerUpBase
{
    public override void Activate()
    {
        m_player.m_fuel += 25;
        base.Activate();
    }
}
