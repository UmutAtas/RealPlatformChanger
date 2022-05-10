using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Tools;

public class PlatformMover : MonoBehaviour
{
    private Vector3 leftPosition;
    private Vector3 midPosition;
    private Vector3 rightPosition;

    private Vector3 leftScale;
    private Vector3 midScale;

    private float moveTime = 1f;
    private float nextMoveTime = 1f;
    [SerializeField] private PlatformController _platformController;
    
    public PlatformController.PlatformState pState;

    private void Awake()
    {
        leftPosition = _platformController.leftPosition;
        midPosition = _platformController.midPosition;
        rightPosition = _platformController.rightPosition;
        leftScale = _platformController.leftScale;
        midScale = _platformController.midScale;
        moveTime = _platformController.moveTime;
        nextMoveTime = _platformController.nextMoveTime;
    }

    public void MoveLeft()
    {
        transform.DOLocalMove(leftPosition, moveTime);
        transform.DOScale(leftScale, moveTime).OnComplete(() =>
        {
            StartCoroutine(ChangeState(PlatformController.PlatformState.left));
        });
    }

    public void MoveRight()
    {
        transform.DOLocalMove(rightPosition, moveTime).OnComplete(() =>
        {
            StartCoroutine(ChangeState(PlatformController.PlatformState.right));
        });
    }

    public void MoveMid()
    {
        transform.DOLocalMove(midPosition, moveTime);
        transform.DOScale(midScale, moveTime).OnComplete(() =>
        {
            StartCoroutine(ChangeState(PlatformController.PlatformState.mid));
        });
    }

    public void MoveAll()
    {
        switch (pState)
        {
            case PlatformController.PlatformState.right:
                MoveMid();
                break;
            case PlatformController.PlatformState.mid:
                MoveLeft();
                break;
            case PlatformController.PlatformState.left:
                MoveRight();
                break;
        }
    }

    public IEnumerator ChangeState(PlatformController.PlatformState statePosition)
    {
        yield return new WaitForSeconds(nextMoveTime);
        pState = statePosition;
    }
    
}
