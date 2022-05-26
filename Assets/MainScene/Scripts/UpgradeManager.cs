using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public int moneyAmount;

    [SerializeField] private int moneyUpgradeAmount;
    [SerializeField] private float speedUpgradeAmount, staminaUpgradeAmount;

    [SerializeField] private int moneyUpgradeCost;
    [SerializeField] private int staminaUpgradeCost;
    [SerializeField] private int speedUpgradeCost;
    [SerializeField] private float increaseUpgradeCostPercent;

    [SerializeField]
    private TextMeshProUGUI moneyLevelTxt, staminaLevelTxt, speedLevelTxt, moneyPriceTxt, staminaPriceTxt, speedPriceTxt;

    [SerializeField] private CanvasGroup moneyCanvasGroup, staminaCanvasGroup, speedCanvasGroup;

    private int moneyLevel = 1, staminaLevel = 1, speedLevel = 1;
    [SerializeField] private float disabledAlpha;

    private void Start()
    {
        // getint("name" , defaultvalue)
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
        
        GetCanvasGroupAlpha(moneyPriceTxt,moneyCanvasGroup);
        GetCanvasGroupAlpha(staminaPriceTxt,staminaCanvasGroup);
        GetCanvasGroupAlpha(speedPriceTxt,speedCanvasGroup);
        
        UpgradeLevelText(moneyLevelTxt, moneyLevel, "MoneyLevel");
        UpgradeLevelText(staminaLevelTxt, staminaLevel, "StaminaLevel");
        UpgradeLevelText(speedLevelTxt, speedLevel, "SpeedLevel");
        UpgradePriceText(moneyPriceTxt, moneyUpgradeCost, "MoneyLevel");
        UpgradePriceText(staminaPriceTxt, staminaUpgradeCost, "StaminaLevel");
        UpgradePriceText(speedPriceTxt, speedUpgradeCost, "SpeedLevel");
    }

    private void GetCanvasGroupAlpha(TextMeshProUGUI priceKind, CanvasGroup canvasGroupKind)
    {
        var coin = PlayerPrefs.GetInt("Coin");
        var upgradeCost = int.Parse(priceKind.text);
        if (coin < upgradeCost && GameManager.Instance.Gamestate == GameManager.GAMESTATE.Start)
            canvasGroupKind.alpha = disabledAlpha;
        else
            canvasGroupKind.alpha = 1;
    }

    public void MoneyUpgradeButton()
    {
        var upgradeCost = int.Parse(moneyPriceTxt.text);
        if (PlayerPrefs.GetInt("Coin") < upgradeCost)
            return;
        var currentMoney = PlayerPrefs.GetInt("MoneyAmount");
        PlayerPrefs.SetInt("MoneyAmount", currentMoney + moneyUpgradeAmount);
        moneyAmount = PlayerPrefs.GetInt("MoneyAmount");
        var currentMoneyLevel = PlayerPrefs.GetInt("MoneyLevel");
        PlayerPrefs.SetInt("MoneyLevel", currentMoneyLevel + 1);
        UpgradeLevelText(moneyLevelTxt, moneyLevel, "MoneyLevel");
        UpgradePriceText(moneyPriceTxt, moneyUpgradeCost, "MoneyLevel");
        var coin = PlayerPrefs.GetInt("Coin");
        PlayerPrefs.SetInt("Coin", coin - upgradeCost);
        CheckButtonAvailable();
    }
    
    public void StaminaUpgradeButton()
    {
        var upgradeCost = int.Parse(staminaPriceTxt.text);
        if (PlayerPrefs.GetInt("Coin") < upgradeCost)
            return;
        var currentStamina = PlayerPrefs.GetFloat("Stamina");
        PlayerPrefs.SetFloat("Stamina", currentStamina + staminaUpgradeAmount);
        PlayerController.Instance.SetPlayerStamina();
        var currentStaminaLevel = PlayerPrefs.GetInt("StaminaLevel");
        PlayerPrefs.SetInt("StaminaLevel", currentStaminaLevel + 1);
        UpgradeLevelText(staminaLevelTxt, staminaLevel, "StaminaLevel");
        UpgradePriceText(staminaPriceTxt, staminaUpgradeCost, "StaminaLevel");
        var coin = PlayerPrefs.GetInt("Coin");
        PlayerPrefs.SetInt("Coin", coin - upgradeCost);
        CheckButtonAvailable();
    }
    
    public void SpeedUpgradeButton()
    {
        var upgradeCost = int.Parse(speedPriceTxt.text);
        if (PlayerPrefs.GetInt("Coin") < upgradeCost)
            return;
        var currentSpeed = PlayerPrefs.GetFloat("Speed");
        PlayerPrefs.SetFloat("Speed", currentSpeed + speedUpgradeAmount);
        PlayerController.Instance.SetPlayerSpeed();
        var currentSpeedLevel = PlayerPrefs.GetInt("SpeedLevel");
        PlayerPrefs.SetInt("SpeedLevel", currentSpeedLevel + 1);
        UpgradeLevelText(speedLevelTxt, speedLevel, "SpeedLevel");
        UpgradePriceText(speedPriceTxt, speedUpgradeCost, "SpeedLevel");
        var coin = PlayerPrefs.GetInt("Coin");
        PlayerPrefs.SetInt("Coin", coin - upgradeCost);
        CheckButtonAvailable();
    }

    private void CheckButtonAvailable()
    {
        GetCanvasGroupAlpha(moneyPriceTxt,moneyCanvasGroup);
        GetCanvasGroupAlpha(staminaPriceTxt,staminaCanvasGroup);
        GetCanvasGroupAlpha(speedPriceTxt,speedCanvasGroup);
    }
    
    private void UpgradeLevelText(TextMeshProUGUI levelText, int intToSet , string prefName)
    {
        intToSet = PlayerPrefs.GetInt(prefName);
        levelText.text = "Level " + intToSet;
    }
    
    private void UpgradePriceText(TextMeshProUGUI priceText, int upgradeKind, string prefName)
    {
        var upgradeLevelIndex = PlayerPrefs.GetInt(prefName);
        if (upgradeLevelIndex == 1)
        {
            priceText.text = upgradeKind.ToString();
        }
        else
        {
            upgradeKind = (int) (upgradeKind * (increaseUpgradeCostPercent * upgradeLevelIndex));
            priceText.text = upgradeKind.ToString();
        }
    }
}
