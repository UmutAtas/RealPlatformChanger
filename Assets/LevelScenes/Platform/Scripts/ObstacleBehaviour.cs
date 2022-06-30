using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    [SerializeField] private float angularVelocity;
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
        rb.angularVelocity = Vector3.up * angularVelocity;
    }
}

