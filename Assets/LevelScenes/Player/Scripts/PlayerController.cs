using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : SingletonPersistent<PlayerController>
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private CinemachineDollyCart _dollyCart;
    public  PlayerMovement _playerMovement;

    private void Start()
    {
        _dollyCart.enabled = false;
        GetPlayerStamina();
        GetPlayerSpeed();
    }

    private void Update()
    {
        if (GameManager.Instance.Gamestate == GameManager.GAMESTATE.Ingame)
        {
            MovePlayerAnimation();
            SetDollySpeed();
        }
    }

    private void SetDollySpeed()
    {
        _dollyCart.m_Speed = _playerMovement._speed;
    }

    public  void GetPlayerStamina()
    {
        _playerMovement._stamina = PlayerPrefs.GetFloat("Stamina" , 100f);
        PlayerPrefs.SetFloat("Stamina" , _playerMovement._stamina);
        _playerMovement.maxStamina = _playerMovement._stamina;
    }

    public void GetPlayerSpeed()
    {
        _playerMovement._speed = PlayerPrefs.GetFloat("Speed", 1.3f);
        PlayerPrefs.SetFloat("Speed" , _playerMovement._speed);
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
