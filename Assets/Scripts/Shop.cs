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
        GetMoney();
        PlayerSettings.Instance.GetVariables();
        CheckText();
        foreach (GameObject powerUpgrades in m_powerUpUpgrades)
        {
            powerUpgrades.SetActive(false);
        }
    }

    void Update()
    {
        #region PowerUpUpgrades

        // Power Up variabels in playersettings    m_shieldPower, m_healthMax, m_specialPower, m_fuelLevel;
        if (PlayerSettings.Instance.m_shieldPower == 1)
        {
            m_powerUpUpgrades[0].SetActive(true);
        }
        else if (PlayerSettings.Instance.m_shieldPower == 2)
        {
            m_powerUpUpgrades[1].SetActive(true);
        }

        if (PlayerSettings.Instance.m_specialPower == 1)
        {
            m_powerUpUpgrades[2].SetActive(true);
        }
        else if (PlayerSettings.Instance.m_specialPower == 2)
        {
            m_powerUpUpgrades[3].SetActive(true);
        }

        if (PlayerSettings.Instance.m_healthMax == 1)
        {
            m_powerUpUpgrades[4].SetActive(true);
        }

        if (PlayerSettings.Instance.m_fuelLevel == 1)
        {
            m_powerUpUpgrades[5].SetActive(true);
        }
        else if (PlayerSettings.Instance.m_fuelLevel == 2)
        {
            m_powerUpUpgrades[6].SetActive(true);
        }
        #endregion
    }

    #region Upgrades
    public void UpgradeShield()
    {
        if(PlayerSettings.Instance.m_money >= PlayerSettings.Instance.m_cost && PlayerSettings.Instance.m_shieldPower != 2)
        {
            PlayerSettings.Instance.m_money -= PlayerSettings.Instance.m_cost;
            PlayerSettings.Instance.m_shieldPower++;
            GetMoney();
            UpgradeCost();
        }
    }

    public void UpgradeHealthPack()
    {
        if (PlayerSettings.Instance.m_money >= PlayerSettings.Instance.m_cost && PlayerSettings.Instance.m_healthMax != 1)
        {
            PlayerSettings.Instance.m_money -= PlayerSettings.Instance.m_cost;
            PlayerSettings.Instance.m_healthMax++;
            GetMoney();
            UpgradeCost();
        }
    }

    public void UpgradeSpecial()
    {
        if (PlayerSettings.Instance.m_money >= PlayerSettings.Instance.m_cost && PlayerSettings.Instance.m_specialPower != 2)
        {
            PlayerSettings.Instance.m_money -= PlayerSettings.Instance.m_cost;
            PlayerSettings.Instance.m_specialPower++;
            GetMoney();
            UpgradeCost();
        }
    }

    public void UpgradeFuel()
    {
        if (PlayerSettings.Instance.m_money >= PlayerSettings.Instance.m_cost && PlayerSettings.Instance.m_fuelLevel != 2)
        {
            PlayerSettings.Instance.m_money -= PlayerSettings.Instance.m_cost;
            PlayerSettings.Instance.m_fuelLevel++;
            GetMoney();
            UpgradeCost();
        }
    }
    #endregion
    
    private void UpgradeCost()
    {
        PlayerSettings.Instance.m_cost += 25;
        CheckText();
    }

    private void CheckText()
    {
        foreach (TextMeshProUGUI buttonText in m_buttonText)
        {   
            buttonText.text = PlayerSettings.Instance.m_cost.ToString();
        }
    }

    private void GetMoney()
    {
        PlayerPrefs.GetInt("Current Money", PlayerSettings.Instance.m_money);

        if (PlayerSettings.Instance.m_money < 1000)
        {
            m_moneyText.text = "Money: 0" + PlayerSettings.Instance.m_money.ToString();
        }
        else
        {
            m_moneyText.text = "Money: " + PlayerSettings.Instance.m_money.ToString();
        }
    }

}
