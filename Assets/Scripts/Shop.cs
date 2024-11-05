using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.SocialPlatforms.Impl;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_moneyText;

    [Header("Power Up Upgrades")]

    [SerializeField]
    private GameObject[] m_powerUpUpgrades;

    [SerializeField]
    private TextMeshProUGUI[] m_buttonText;



    void Start()
    {
        GameManager.Instance.LoadSaveData();
        GetMoney();
        CheckText();
        foreach (GameObject powerUpgrades in m_powerUpUpgrades)
        {
            powerUpgrades.SetActive(false);
        }
    }

    void Update()
    {
        #region PowerUpUpgrades
        // Power Up variabels in GameManager   m_shieldPower, m_healthMax, m_specialPower, m_fuelLevel;
        if (GameManager.Instance.m_shieldPower == 1)
        {
            m_powerUpUpgrades[0].SetActive(true);
        }
        else if (GameManager.Instance.m_shieldPower == 2)
        {
            m_powerUpUpgrades[1].SetActive(true);
        }

        if (GameManager.Instance.m_specialPower == 1)
        {
            m_powerUpUpgrades[2].SetActive(true);
        }
        else if (GameManager.Instance.m_specialPower == 2)
        {
            m_powerUpUpgrades[3].SetActive(true);
        }

        if (GameManager.Instance.m_healthMax == 1)
        {
            m_powerUpUpgrades[4].SetActive(true);
        }

        if (GameManager.Instance.m_fuelLevel == 1)
        {
            m_powerUpUpgrades[5].SetActive(true);
        }
        else if (GameManager.Instance.m_fuelLevel == 2)
        {
            m_powerUpUpgrades[6].SetActive(true);
        }
        #endregion
    }

    #region Upgrades
    public void UpgradeShield()
    {
        if(GameManager.Instance.m_money >= GameManager.Instance.m_cost && GameManager.Instance.m_shieldPower != 2)
        {
            GameManager.Instance.m_money -= GameManager.Instance.m_cost;
            GameManager.Instance.m_shieldPower++;
            GetMoney();
            UpgradeCost();
        }
    }

    public void UpgradeHealthPack()
    {
        if (GameManager.Instance.m_money >= GameManager.Instance.m_cost && GameManager.Instance.m_healthMax != 1)
        {
            GameManager.Instance.m_money -= GameManager.Instance.m_cost;
            GameManager.Instance.m_healthMax++;
            GetMoney();
            UpgradeCost();
        }
    }

    public void UpgradeSpecial()
    {
        if (GameManager.Instance.m_money >= GameManager.Instance.m_cost && GameManager.Instance.m_specialPower != 2)
        {
            GameManager.Instance.m_money -= GameManager.Instance.m_cost;
            GameManager.Instance.m_specialPower++;
            GetMoney();
            UpgradeCost();
        }
    }

    public void UpgradeFuel()
    {
        if (GameManager.Instance.m_money >= GameManager.Instance.m_cost && GameManager.Instance.m_fuelLevel != 2)
        {
            GameManager.Instance.m_money -= GameManager.Instance.m_cost;
            GameManager.Instance.m_fuelLevel++;
            GetMoney();
            UpgradeCost();
        }
    }
    #endregion
    
    private void UpgradeCost()
    {
        GameManager.Instance.m_cost += 25;
        CheckText();
    }

    private void CheckText()
    {
        foreach (TextMeshProUGUI buttonText in m_buttonText)
        {   
            buttonText.text = GameManager.Instance.m_cost.ToString();
        }
    }

    public void SetVariables()
    {
        GameManager.Instance.SaveGame();
    }


    private void GetMoney()
    {
        PlayerPrefs.GetInt("Current Money", GameManager.Instance.m_money);

        if (GameManager.Instance.m_money < 1000)
        {
            m_moneyText.text = "Money: 0" + GameManager.Instance.m_money.ToString();
        }
        else
        {
            m_moneyText.text = "Money: " + GameManager.Instance.m_money.ToString();
        }
    }

}
