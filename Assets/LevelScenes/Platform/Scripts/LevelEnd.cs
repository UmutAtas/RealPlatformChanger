using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private float waitForGameFinish;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 3)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            PlayerController.Instance.isLevelEnd = true;
            StartCoroutine(GameFinishRoutine());
        }
    }

    private IEnumerator GameFinishRoutine()
    {
        yield return new WaitForSeconds(waitForGameFinish);
        GameManager.Instance.Gamestate = GameManager.GAMESTATE.Finish;
        PlayerController.Instance.isLevelEnd = false;
    }
}
