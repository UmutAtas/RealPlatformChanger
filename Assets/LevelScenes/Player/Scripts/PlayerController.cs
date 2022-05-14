using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private CinemachineDollyCart _dollyCart;

    private void Start()
    {
        _dollyCart.enabled = false;
    }

    private void Update()
    {
        MovePlayerAnimation();
    }

    private void MovePlayerAnimation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAnimator.SetBool("isTap", true);
            _dollyCart.enabled = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            playerAnimator.SetBool("isTap", false);
            _dollyCart.enabled = false;
        }
    }
}
