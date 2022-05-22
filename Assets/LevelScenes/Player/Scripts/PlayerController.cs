using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private CinemachineDollyCart _dollyCart;
    public PlayerMovement _playerMovement;

    private void Start()
    {
        _dollyCart.enabled = false;
    }

    private void Update()
    {
        MovePlayerAnimation();
        SetDollySpeed();
    }

    private void SetDollySpeed()
    {
        _dollyCart.m_Speed = _playerMovement._speed;
    }

    public void SetPlayerStamina()
    {
        _playerMovement._stamina = PlayerPrefs.GetFloat("Stamina");
        _playerMovement.maxStamina = _playerMovement._stamina;
    }

    public void SetPlayerSpeed()
    {
        _playerMovement._speed = PlayerPrefs.GetFloat("Speed");
        _playerMovement.baseSpeed = _playerMovement._speed;
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
