using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Turn();
    }

    private void Turn()
    {
        rb.AddTorque(Vector3.up * 2);
    }
}

