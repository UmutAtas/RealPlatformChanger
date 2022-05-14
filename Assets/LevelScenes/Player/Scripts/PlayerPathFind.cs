using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerPathFind : MonoBehaviour
{
    [SerializeField] private CinemachineDollyCart _dollyCart;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            other.gameObject.layer = 8;
            if (other.TryGetComponent(out CinemachineSmoothPath path))
            {
                if (_dollyCart.m_Path != path)
                {
                    _dollyCart.m_Path = path;
                    _dollyCart.m_Position = 0f;
                    print("girdi2");
                }
            }
        }
    }
}
