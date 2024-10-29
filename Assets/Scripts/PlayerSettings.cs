using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerSettings : Singleton<PlayerSettings>
{
    public int m_score;
    public int m_money = 25;
    public int m_cost = 25;
    public int m_maxFuel;
    public string m_IGN;

    //Power up changes
    public int m_shieldPower, m_healthMax, m_specialPower, m_fuelLevel;

    public void MaxFuel()
    {
        if (m_fuelLevel == 1)
        {
            m_maxFuel = 125;
        }

        else if (m_fuelLevel == 2)
        {
            m_maxFuel = 150;
        }

        else
        {
            m_maxFuel = 100;
        }
    }

    #region PlayerPrefs
    public void GetVariables()
    {
        PlayerPrefs.GetInt("Current Money", m_money);
        PlayerPrefs.GetInt("Shield Level", m_shieldPower);
        PlayerPrefs.GetInt("Special Power", m_specialPower);
        PlayerPrefs.GetInt("Health Powerup", m_healthMax);
        PlayerPrefs.GetInt("Fuel Level", m_fuelLevel);
        PlayerPrefs.GetInt("Cost", m_cost);
    }

    public void SetVariables()
    {
        PlayerPrefs.SetInt("Current Money", m_money);
        PlayerPrefs.SetInt("Shield Level", m_shieldPower);
        PlayerPrefs.SetInt("Special Power", m_specialPower);
        PlayerPrefs.SetInt("Health Powerup", m_healthMax);
        PlayerPrefs.SetInt("Fuel Level", m_fuelLevel);

    }
    #endregion
}
