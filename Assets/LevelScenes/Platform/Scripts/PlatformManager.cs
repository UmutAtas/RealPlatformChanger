using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : Singleton<PlatformManager>
{
    public List<PlatformController> platformControllerList = new List<PlatformController>();
    
    [SerializeField] private float platformMoveDelayTime = 1f;

    private void Start()
    {
        StartCoroutine(PlatformMoveDelay());
    }

    private void Update()
    {
        LockPlatformPosition();
    }

    private void LockPlatformPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            platformControllerList[0].isTap = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {   
            platformControllerList[0].isTap = false;
        }
    }
    
    private IEnumerator PlatformMoveDelay()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<PlatformController>().canMove = true;
            yield return new WaitForSeconds(platformMoveDelayTime);
        }
    }
}
