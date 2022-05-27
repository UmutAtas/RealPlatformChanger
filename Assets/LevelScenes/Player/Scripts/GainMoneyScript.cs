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
    [SerializeField] private float particleLifeTime;
    [SerializeField] private float upwardsMod;

    private void Start()
    {
        GetMoneyAmount();
        MoneyAnimation();
    }

    private void GetMoneyAmount()
    {
        moneyText.text = "+ " + ButtonManager.Instance.MoneyAmount.ToString() + "  .  0" + "   $";
    }

    private void MoneyAnimation()
    {
        var color = moneyText.color;
        color.a = 0;
        moneyText.DOColor(color, particleLifeTime).SetEase(Ease.InCubic);
        transform.DOLocalMove(transform.localPosition + (Vector3.up * upwardsMod), particleLifeTime).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
