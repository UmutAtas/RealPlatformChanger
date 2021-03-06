using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    [SerializeField]GameObject StartP, InGameP, NextP, GameOverP;
    [SerializeField] private TextMeshProUGUI m_CoinText; 
    TextMeshProUGUI m_LevelText;
    [SerializeField]Sprite MuteOn, MuteOff, TapticOn, TapticOff;
    GameObject m_Settings;
    [HideInInspector]
    public int m_Coin;

    [SerializeField] private Transform moneyImageTransform;
    [SerializeField] private float moneyImageScaleDuration;

    void Start()
    {
        m_LevelText = InGameP.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        m_Settings = InGameP.transform.GetChild(1).GetChild(0).gameObject;
        m_LevelText.text = "LEVEL " + PlayerPrefs.GetInt("Level", 1);
        m_Coin = PlayerPrefs.GetInt("Coin", 0);
        SetCoin(0);
    }
    public void PanelController(GameManager.GAMESTATE currentPanel)
    {
        StartP.SetActive(false);
        NextP.SetActive(false);
        GameOverP.SetActive(false);
        InGameP.SetActive(false);
        switch (currentPanel)
        {
            case GameManager.GAMESTATE.Start:
                StartP.SetActive(true);
                break;
            case GameManager.GAMESTATE.Ingame:
                InGameP.SetActive(true);
                break;
            case GameManager.GAMESTATE.GameOver:
                GameOverP.SetActive(true);
                break;
            case GameManager.GAMESTATE.Finish:
                NextP.SetActive(true);
                break;
        }
    }
    public void SetLevel(int amount)
    {
        PlayerPrefs.SetInt("Coin",m_Coin);
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 1) + amount);
        m_LevelText.text = "LEVEL " + PlayerPrefs.GetInt("Level", 1);
    }
    public void Settings()
    {
        if(m_Settings.activeInHierarchy)
            m_Settings.SetActive(false);
        else 
            m_Settings.SetActive(true);
    }
    public void Mute()
    {
        var component = Camera.main.GetComponent<AudioListener>();
        component.enabled = !component.isActiveAndEnabled;
        m_Settings.transform.GetChild(1).GetComponent<Image>().sprite = IconChanger(MuteOn, MuteOff, component.isActiveAndEnabled);
    }
    public void Taptic()
    {
        GameManager.Instance.taptic = !GameManager.Instance.taptic;
        m_Settings.transform.GetChild(0).GetComponent<Image>().sprite = IconChanger(TapticOn, TapticOff, GameManager.Instance.taptic);
    }
    Sprite IconChanger(Sprite first, Sprite second,bool state)
    {
        return state ? first : second;
    }
    public void SetCoin(int value)
    {
        m_Coin += value;
        m_CoinText.text = m_Coin.ToString();
        SetLevel(0);
    } // Add or Remove Coin

    public void ChangeMoneyImageScale()
    {
        moneyImageTransform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), moneyImageScaleDuration).OnComplete(() =>
        {
            moneyImageTransform.DOScale(new Vector3(1f, 1f, 1f), moneyImageScaleDuration);
        });
    }
}
