using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GainMoneyScript : MonoBehaviour
{
    [SerializeField] private TextMeshPro moneyText;
    [SerializeField] private float colorAlphaTime;
    [SerializeField] private float upwardsMod;

    private void Start()
    {
        GetMoneyAmount();
        MoneyAnimation();
    }

    private void Update()
    {
        transform.eulerAngles = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }

    private void GetMoneyAmount()
    {
        moneyText.text = ButtonManager.Instance.MoneyAmount.ToString() + ".0";
    }

    private void MoneyAnimation()
    {
        var color = moneyText.color;
        color.a = 0;
        moneyText.DOColor(color, colorAlphaTime).SetEase(Ease.InCubic);
        transform.DOLocalMove(transform.localPosition + (Vector3.up * upwardsMod), 1f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
