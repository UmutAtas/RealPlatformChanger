using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Tools;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] private Vector3 leftPosition;
    [SerializeField] private Vector3 midPosition;
    [SerializeField] private Vector3 rightPosition;

    [SerializeField] private Vector3 leftScale;
    [SerializeField] private Vector3 midScale;

    [SerializeField] private float moveTime = 1f;
    [SerializeField] private float nextMoveTime = 1f;
    
    public PlatformController.PlatformState pState;

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
