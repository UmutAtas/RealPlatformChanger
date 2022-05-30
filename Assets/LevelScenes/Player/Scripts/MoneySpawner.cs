using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawner : MonoBehaviour
{
    [SerializeField] private float moneySpawnDistance = 7 / 20f;
    private float _currentDistance , _position;
    
    [SerializeField] private GameObject particleToSpawn;
    [SerializeField] private Vector3 moneySpawnOffset;
    [SerializeField] private Transform moneyParent;
    private void Awake()
    {
        _position = transform.position.z;
    }

    private void Update()
    {
        MoneySpawnerTimer();
    }

    private void MoneySpawnerTimer()
    {
        var currentPos = transform.position.z;
        if (_currentDistance < moneySpawnDistance)
            _currentDistance = currentPos - _position;
        else
        {
            //event kullan
            Instantiate(particleToSpawn, transform.position + moneySpawnOffset, new Quaternion(0,0,0,1), moneyParent);
            _currentDistance = 0f;
            _position = currentPos;
            UIManager.Instance.SetCoin(ButtonManager.Instance.MoneyAmount);
            UIManager.Instance.ChangeMoneyImageScale();
            if (GameManager.Instance.taptic)
                Taptic.Medium();
        }
    }
}
