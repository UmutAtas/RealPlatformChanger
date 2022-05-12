using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runTime = 5f;
    [SerializeField] private float slowDownTime = 2f;
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float slowDownSpeed = 0.2f;

    public bool canSlowDown;
    public bool canTired;

    private float startMovementSpeed;

    private void Awake()
    {
        startMovementSpeed = movementSpeed;
    }

    private void Update()
    {
        MovePlayer();
    }

    private IEnumerator RunCoroutine()
    {
        canSlowDown = false;
        canTired = false;
        float i = 0f;
        movementSpeed = startMovementSpeed;
        while (i < runTime)
        {
            i += Time.deltaTime;
            transform.Translate(Vector3.forward * (movementSpeed * Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }
        canSlowDown = true;
        StartCoroutine(SlowDownCoroutine());
    }

    private IEnumerator SlowDownCoroutine()
    {
        canTired = false;
        if (canSlowDown)
        {
            float i = 0f;
            while (i < slowDownTime)
            {
                i += Time.deltaTime;
                if (movementSpeed > 0f)
                {
                    movementSpeed -= slowDownSpeed;
                }
                transform.Translate(Vector3.forward * (movementSpeed * Time.deltaTime));
                yield return new WaitForEndOfFrame();
            }
            canTired = true;
        }
        Tired();
    }

    private void Tired()
    {
        if (canTired)
        {
            movementSpeed = 0f;
            transform.Translate(Vector3.forward * movementSpeed);
        }
    }

    private void MovePlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(RunCoroutine());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopAllCoroutines();
        }
    }
}
