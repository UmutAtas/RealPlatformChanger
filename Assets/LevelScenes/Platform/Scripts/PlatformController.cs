using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private List<Transform> platformList = new List<Transform>();

    public enum PlatformState
    {
        right,
        mid,
        left,
    }
}
