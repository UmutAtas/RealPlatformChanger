using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    private void Update()
    {
        MovePlayerAnimation();
    }

    private void MovePlayerAnimation()
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
