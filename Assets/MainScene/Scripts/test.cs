using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class test : MonoBehaviour
{
    [SerializeField]float MoneyUpgrade, SpeedUpgrade, StaminaUpgrade;
    private int _moneylevel, _speedlevel, _staminalevel, _moneycost, _speedcost, _staminacost;

    int MoneyCost
    {
        get
        {
            return _moneycost;
        }
        set
        {
            _moneycost = value;
            moneyPriceTxt.text = _moneycost.ToString();
            PlayerPrefs.SetInt("Money", _moneycost);
        }
    }
    int MoneyLevel
    {
        get
        {
            return _moneylevel;
        }
        set
        {
            _moneylevel = value;
            moneyLevelTxt.text = "Level " + _moneylevel;
            PlayerPrefs.SetInt("MoneyLevel", _moneylevel);
        }
    }

    int SpeedCost
    {
        get
        {
            return _speedcost;
        }
        set
        {
            _speedcost = value;
            speedPriceTxt.text = _speedcost.ToString();
            PlayerPrefs.SetInt("Speed", _speedcost);
        }
    }
    int SpeedLevel
    {
        get
        {
            return _speedlevel;
        }
        set
        {
            _speedlevel = value;
            speedLevelTxt.text = "Level " + _speedlevel;
            PlayerPrefs.SetInt("SpeedLevel", _speedlevel);
        }
    }

    int StaminaCost
    {
        get
        {
            return _staminacost;
        }
        set
        {
            _staminacost = value;
            staminaPriceTxt.text = _staminacost.ToString();
            PlayerPrefs.SetInt("Stamina", _staminacost);
        }
    }
    int StaminaLevel
    {
        get
        {
            return _staminalevel;
        }
        set
        {
            _staminalevel = value;
            staminaLevelTxt.text = "Level " + _staminalevel;
            PlayerPrefs.SetInt("StaminaLevel", _staminalevel);
        }
    }
    
    [SerializeField] TextMeshProUGUI moneyLevelTxt, staminaLevelTxt, speedLevelTxt, moneyPriceTxt, staminaPriceTxt, speedPriceTxt;
    void Start()
    {
        MoneyCost = PlayerPrefs.GetInt("Money", 50);
        SpeedCost = PlayerPrefs.GetInt("Speed", 50);
        StaminaCost = PlayerPrefs.GetInt("Stamina", 50);
        MoneyLevel = PlayerPrefs.GetInt("MoneyLevel",1);
        SpeedLevel = PlayerPrefs.GetInt("SpeedLevel", 1);
        StaminaLevel = PlayerPrefs.GetInt("StaminaLevel", 1);
    }
    public void BuyMoney()
    {
        if(MoneyCost > UIManager.Instance.m_Coin)
            return;
        UIManager.Instance.SetCoin(-MoneyCost);
        MoneyCost += MoneyCost/MoneyLevel;
        MoneyLevel += 1;
    }
    public void BuySpeed()
    {
        if(SpeedCost > UIManager.Instance.m_Coin)
            return;
        UIManager.Instance.SetCoin(-SpeedCost);
        SpeedCost += SpeedCost/SpeedLevel;
        SpeedLevel += 1;
    }
    public void BuyStamina()
    {
        if(StaminaCost > UIManager.Instance.m_Coin)
            return;
        UIManager.Instance.SetCoin(-StaminaCost);
        StaminaCost += (StaminaCost / StaminaLevel);
        StaminaLevel += 1;
    }
}
