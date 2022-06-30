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
    //[SerializeField] private float platformMoveDelayTime = 1f;

    [SerializeField] private List<PlatformMover> platformList = new List<PlatformMover>();
    //[SerializeField] private Transform allPlatforms;
    public Transform oldPlatforms;

    public bool isTap;
    public bool canMove;
    public bool underPlayer;
    public enum PlatformState
    {
        right,
        mid,
        left,
    }
    
    private void FixedUpdate()
    {
        if (!isTap && !underPlayer && canMove)
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
}
