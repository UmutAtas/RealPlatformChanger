using System;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
public class ButtonManager : Singleton<ButtonManager>
{
    [SerializeField]float MoneyUpgrade, SpeedUpgrade, StaminaUpgrade;

    private int _moneylevel,
        _speedlevel,
        _staminalevel,
        _moneycost,
        _speedcost,
        _staminacost,
        _moneyamount;

    private float _speed, _stamina;
    [SerializeField] private float disabledAlpha;
    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
            PlayerPrefs.SetFloat("Speed",_speed);
        }
    }
    public float Stamina
    {
        get
        {
            return _stamina;
        }
        set
        {
            _stamina = value;
            PlayerPrefs.SetFloat("Stamina",_stamina);
        }
    }
    public int MoneyAmount
    {
        get
        {
            return _moneyamount;
        }
        set
        {
            _moneyamount = value;
            PlayerPrefs.SetInt("MoneyAmount",_moneyamount);
        }
    }
    
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
            PlayerPrefs.SetInt("MoneyCost", _moneycost);
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
            PlayerPrefs.SetInt("SpeedCost", _speedcost);
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
            PlayerPrefs.SetInt("StaminaCost", _staminacost);
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
    
    [SerializeField] private CanvasGroup moneyCanvasGroup, staminaCanvasGroup, speedCanvasGroup;
    [SerializeField] TextMeshProUGUI moneyLevelTxt, staminaLevelTxt, speedLevelTxt, moneyPriceTxt, staminaPriceTxt, speedPriceTxt;
    void Start()
    {
        EventManager.Instance.OnCoin += CheckCoin;
        MoneyCost = PlayerPrefs.GetInt("MoneyCost", 50);
        SpeedCost = PlayerPrefs.GetInt("SpeedCost", 50);
        StaminaCost = PlayerPrefs.GetInt("StaminaCost", 50);
        MoneyLevel = PlayerPrefs.GetInt("MoneyLevel",1);
        SpeedLevel = PlayerPrefs.GetInt("SpeedLevel", 1);
        StaminaLevel = PlayerPrefs.GetInt("StaminaLevel", 1);
        MoneyAmount = PlayerPrefs.GetInt("MoneyAmount", 1);
        _speed = PlayerPrefs.GetFloat("Speed", 1.3f);
        _stamina = PlayerPrefs.GetFloat("Stamina", 100);
    }
    private void OnDisable()
    {
        EventManager.Instance.OnCoin -= CheckCoin;
    }
    void CheckCoin()
    {
        moneyCanvasGroup.alpha = _moneycost > UIManager.Instance.m_Coin ? disabledAlpha : 1;
        speedCanvasGroup.alpha = _speedcost > UIManager.Instance.m_Coin ? disabledAlpha : 1;
        staminaCanvasGroup.alpha = _staminacost > UIManager.Instance.m_Coin ? disabledAlpha : 1;
    }
    
    public void BuyMoney()
    {
        if(MoneyCost > UIManager.Instance.m_Coin)
            return;
        UIManager.Instance.SetCoin(-MoneyCost);
        MoneyCost += MoneyCost/MoneyLevel;
        MoneyLevel += 1;
        MoneyAmount += (int)MoneyUpgrade;
    }
    
    public void BuySpeed()
    {
        if(SpeedCost > UIManager.Instance.m_Coin)
            return;
        UIManager.Instance.SetCoin(-SpeedCost);
        SpeedCost += SpeedCost/SpeedLevel;
        SpeedLevel += 1;
        Speed += SpeedUpgrade;
    }
    
    public void BuyStamina()
    {
        if(StaminaCost > UIManager.Instance.m_Coin)
            return;
        UIManager.Instance.SetCoin(-StaminaCost);
        StaminaCost += (StaminaCost / StaminaLevel);
        StaminaLevel += 1;
        Stamina += StaminaUpgrade;
    }
}
