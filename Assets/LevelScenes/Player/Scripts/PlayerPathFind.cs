using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
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
                    //_dollyCart.transform.DOMove(path.m_Waypoints[0].position, 0.2f).OnComplete(() =>
                    //{
                    //    _dollyCart.m_Path = path;
                    //    _dollyCart.m_Position = 0f;
                    //    print("girdi2");
                    //});
                    _dollyCart.m_Path = path;
                    _dollyCart.m_Position = 0f;
                    print("girdi2");
                }
            }
        }
    }
}
