using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 3)
        {
            GameManager.Instance.Gamestate = GameManager.GAMESTATE.Finish;
        }
    }
}
