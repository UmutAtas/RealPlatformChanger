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

    public bool isTap;
    public enum PlatformState
    {
        right,
        mid,
        left,
    }

    private void Update()
    {
        LockPlatformPosition();
    }

    private void FixedUpdate()
    {
        if (!isTap)
        {
            MovePlatforms();
        }
    }

    private void MovePlatforms()
    {
        foreach (var platform in platformList)
        {
            platform.MoveAll();
        }
    }

    private void LockPlatformPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isTap = true;
            MovePlatforms();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isTap = false;
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
