using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public int moneyAmount;

    [SerializeField] private int newMoneyAmount;
    [SerializeField] private float newSpeed, newStamina;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("MoneyAmount"))
            PlayerPrefs.SetInt("MoneyAmount", moneyAmount);
        moneyAmount = PlayerPrefs.GetInt("MoneyAmount");

        if (!PlayerPrefs.HasKey("Stamina"))
            PlayerPrefs.SetFloat("Stamina", PlayerController.Instance._playerMovement._stamina);

        if (!PlayerPrefs.HasKey("Speed"))
            PlayerPrefs.SetFloat("Speed", PlayerController.Instance._playerMovement._speed);
        
        PlayerController.Instance.SetPlayerSpeed();
        PlayerController.Instance.SetPlayerStamina();
    }
    
    public void MoneyUpgradeButton()
    {
        var currentMoney = PlayerPrefs.GetInt("MoneyAmount");
        PlayerPrefs.SetInt("MoneyAmount", currentMoney + newMoneyAmount);
        moneyAmount = PlayerPrefs.GetInt("MoneyAmount");
    }
    
    public void StaminaUpgradeButton()
    {
        var currentStamina = PlayerPrefs.GetFloat("Stamina");
        PlayerPrefs.SetFloat("Stamina", currentStamina + newStamina);
        PlayerController.Instance.SetPlayerStamina();
    }
    
    public void SpeedUpgradeButton()
    {
        var currentSpeed = PlayerPrefs.GetFloat("Speed");
        PlayerPrefs.SetFloat("Speed", currentSpeed + newSpeed);
        PlayerController.Instance.SetPlayerSpeed();
    }
}
