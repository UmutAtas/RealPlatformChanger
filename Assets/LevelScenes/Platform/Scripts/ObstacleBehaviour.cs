using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Turn();
    }

    private void Turn()
    {
        rb.angularVelocity = Vector3.up * 2;
    }
}

