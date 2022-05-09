using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private List<PlatformMover> platformList = new List<PlatformMover>();

    private bool isTap;
    public enum PlatformState
    {
        right,
        mid,
        left,
    }

    private void FixedUpdate()
    {
        MovePlatforms();
    }

    private void MovePlatforms()
    {
        if (!isTap)
        {
            foreach (var platform in platformList)
            {
                platform.MoveAll();
            }
        }
    }

    private void LockPlatformPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isTap = true;
        }
    }

    //private IEnumerator sdasd()
    //{
    //    foreach (var sad in platformList)
    //    {
    //        sad.MoveAll();
    //        yield return new WaitForSeconds(0.2f);
    //    }
    //}
}
