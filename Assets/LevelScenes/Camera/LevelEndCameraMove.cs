using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LevelEndCameraMove : MonoBehaviour
{
    public CinemachineVirtualCamera cmVirtual;
    private CinemachineTransposer _transposer;
    [SerializeField] private Vector3 levelEndCamOffset;
    [NonSerialized] public static bool cameraCanMove;

    private void Awake()
    {
        _transposer = cmVirtual.GetCinemachineComponent<CinemachineTransposer>();
    }

    private void Start()
    {
        cameraCanMove = false;
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (cameraCanMove)
            _transposer.m_FollowOffset =
                Vector3.Lerp(_transposer.m_FollowOffset, levelEndCamOffset, 5f * Time.deltaTime);
        else
            _transposer.m_FollowOffset = new Vector3(0.35f, 1.5f, -2f);
    }
}
