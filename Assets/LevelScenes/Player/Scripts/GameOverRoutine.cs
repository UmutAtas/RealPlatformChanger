using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverRoutine : MonoBehaviour
{
    [SerializeField] private float waitForGameOver;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float yForce;

    private void Start()
    {
        StartCoroutine(GameOverState());
    }

    private IEnumerator GameOverState()
    {
        rb.AddForce(0f, yForce, 0f);
        yield return new WaitForSeconds(waitForGameOver);
        GameManager.Instance.Gamestate = GameManager.GAMESTATE.GameOver;
        Destroy(gameObject);
    }
}
