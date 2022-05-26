using Cinemachine;
using UnityEngine;

public class PlayerController : SingletonPersistent<PlayerController>
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private CinemachineDollyCart _dollyCart;

    private void Start()
    {
        _dollyCart.enabled = false;
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
        _dollyCart.m_Speed = ButtonManager.Instance.Speed;
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
