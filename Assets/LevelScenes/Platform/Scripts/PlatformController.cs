using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Vector3 leftPosition;
    public Vector3 midPosition;
    public Vector3 rightPosition;
    
    public Vector3 leftScale;
    public Vector3 midScale;
    
    public float moveTime = 1f;
    public float nextMoveTime = 1f;
    
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
