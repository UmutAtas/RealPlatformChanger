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
            Instantiate(particleToSpawn, transform.position + moneySpawnOffset, Quaternion.identity, moneyParent);
            _currentDistance = 0f;
            _position = currentPos;
            UIManager.Instance.SetCoin(ButtonManager.Instance.MoneyAmount);
        }
    }
}
