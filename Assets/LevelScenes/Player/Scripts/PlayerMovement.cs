using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAnimator.SetBool("isTap", true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            playerAnimator.SetBool("isTap", false);
        }
    }
}
