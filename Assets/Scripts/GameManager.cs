using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;


public class GameManager : Singleton<GameManager>
{
    public int m_currentWave = 1;
    public int m_currentGroup;
    public int m_score;
    public int m_money;
    public int m_cost = 25;
    public int m_maxFuel;
    public string m_IGN;

    //Power up changes
    public int m_shieldPower, m_healthMax, m_specialPower, m_fuelLevel;


    protected override void Awake()
    {
        Time.timeScale = 1.0f;
        LoadSaveData();
        base.Awake();
    }

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

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.m_cost = m_cost;
        data.m_money = m_money;
        data.m_maxFuel = m_maxFuel;
        data.m_shieldPower = m_shieldPower;
        data.m_healthMax = m_healthMax;
        data.m_specialPower = m_specialPower;
        data.m_fuelLevel = m_fuelLevel;
        SaveSystem.SerializeData(data);
    }

    public void LoadSaveData()
    {
        SaveData data = SaveSystem.Deserialize();
        m_cost = data.m_cost;
        m_money = data.m_money;
        m_maxFuel = data.m_maxFuel;
        m_shieldPower = data.m_shieldPower;
        m_healthMax = data.m_healthMax;
        m_specialPower = data.m_specialPower;
        m_fuelLevel = data.m_fuelLevel;
    }
}
