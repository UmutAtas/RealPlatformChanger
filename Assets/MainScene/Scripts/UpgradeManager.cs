using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public int moneyAmount;

    [SerializeField] private int newMoneyAmount;
    [SerializeField] private float newSpeed, newStamina;

    [SerializeField] private List<int> moneyUpgradePrice = new List<int>();
    [SerializeField] private List<int> staminaUpgradePrice = new List<int>();
    [SerializeField] private List<int> speedUpgradePrice = new List<int>();

    [SerializeField]
    private TextMeshProUGUI moneyLevelTxt, staminaLevelTxt, speedLevelTxt, moneyPriceTxt, staminaPriceTxt, speedPriceTxt;

    [SerializeField] private List<Image> moneyUpgradeImages = new List<Image>();
    [SerializeField] private List<Image> staminaUpgradeImages = new List<Image>();
    [SerializeField] private List<Image> speedUpgradeImages = new List<Image>();

    private int moneyLevel = 1, staminaLevel = 1, speedLevel = 1;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("MoneyAmount"))
            PlayerPrefs.SetInt("MoneyAmount", moneyAmount);
        moneyAmount = PlayerPrefs.GetInt("MoneyAmount");
        if (!PlayerPrefs.HasKey("Stamina"))
            PlayerPrefs.SetFloat("Stamina", PlayerController.Instance._playerMovement._stamina);
        if (!PlayerPrefs.HasKey("Speed"))
            PlayerPrefs.SetFloat("Speed", PlayerController.Instance._playerMovement._speed);
        if (!PlayerPrefs.HasKey("MoneyLevel"))
            PlayerPrefs.SetInt("MoneyLevel", moneyLevel);
        if (!PlayerPrefs.HasKey("StaminaLevel"))
            PlayerPrefs.SetInt("StaminaLevel", staminaLevel);
        if (!PlayerPrefs.HasKey("SpeedLevel"))
            PlayerPrefs.SetInt("SpeedLevel", speedLevel);
        
        PlayerController.Instance.SetPlayerSpeed();
        PlayerController.Instance.SetPlayerStamina();
        UpgradeLevelText(moneyLevelTxt, moneyLevel, "MoneyLevel");
        UpgradeLevelText(staminaLevelTxt, staminaLevel, "StaminaLevel");
        UpgradeLevelText(speedLevelTxt, speedLevel, "SpeedLevel");
        UpgradePriceText(moneyPriceTxt, moneyLevel, moneyUpgradePrice, "MoneyLevel");
        UpgradePriceText(staminaPriceTxt, staminaLevel, staminaUpgradePrice, "StaminaLevel");
        UpgradePriceText(speedPriceTxt, speedLevel, speedUpgradePrice, "SpeedLevel");
    }

    public void MoneyUpgradeButton()
    {
        var currentMoney = PlayerPrefs.GetInt("MoneyAmount");
        PlayerPrefs.SetInt("MoneyAmount", currentMoney + newMoneyAmount);
        moneyAmount = PlayerPrefs.GetInt("MoneyAmount");
        var currentMoneyLevel = PlayerPrefs.GetInt("MoneyLevel");
        PlayerPrefs.SetInt("MoneyLevel", currentMoneyLevel + 1);
        UpgradeLevelText(moneyLevelTxt, moneyLevel, "MoneyLevel");
        UpgradePriceText(moneyPriceTxt, moneyLevel, moneyUpgradePrice, "MoneyLevel");
    }
    
    public void StaminaUpgradeButton()
    {
        var currentStamina = PlayerPrefs.GetFloat("Stamina");
        PlayerPrefs.SetFloat("Stamina", currentStamina + newStamina);
        PlayerController.Instance.SetPlayerStamina();
        var currentStaminaLevel = PlayerPrefs.GetInt("StaminaLevel");
        PlayerPrefs.SetInt("StaminaLevel", currentStaminaLevel + 1);
        UpgradeLevelText(staminaLevelTxt, staminaLevel, "StaminaLevel");
        UpgradePriceText(staminaPriceTxt, staminaLevel, staminaUpgradePrice, "StaminaLevel");
    }
    
    public void SpeedUpgradeButton()
    {
        var currentSpeed = PlayerPrefs.GetFloat("Speed");
        PlayerPrefs.SetFloat("Speed", currentSpeed + newSpeed);
        PlayerController.Instance.SetPlayerSpeed();
        var currentSpeedLevel = PlayerPrefs.GetInt("SpeedLevel");
        PlayerPrefs.SetInt("SpeedLevel", currentSpeedLevel + 1);
        UpgradeLevelText(speedLevelTxt, speedLevel, "SpeedLevel");
        UpgradePriceText(speedPriceTxt, speedLevel, speedUpgradePrice, "SpeedLevel");
    }
    
    private void UpgradeLevelText(TextMeshProUGUI levelText, int intToSet , string prefName)
    {
        intToSet = PlayerPrefs.GetInt(prefName);
        levelText.text = "Level " + intToSet;
    }

    private void UpgradePriceText(TextMeshProUGUI priceText, int priceIndex , List<int> listToGet , string prefName)
    {
        priceIndex = PlayerPrefs.GetInt(prefName);
        priceText.text = listToGet[priceIndex - 1].ToString();
    }

    private void ButtonColors()
    {
        
    }
}
