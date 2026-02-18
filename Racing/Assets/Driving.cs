using UnityEngine;
using UnityEngine.InputSystem;

public class Driving : MonoBehaviour
{

    private Rigidbody2D rb;
    private CarInput controls;
    private Vector2 MoveInput;

    public float MaxSpeed = 180f;
    public float Acceleration = 10f;
    public float Deceleration = 5f;
    public float SteeringSpeed = 100f;
    public float DriftFactor = 0.2f;

    private void Awake()
    {
        controls = new CarInput();

        // Subscribe to movement input
        controls.Car.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        controls.Car.Move.canceled += ctx => MoveInput = Vector2.zero;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void FixedUpdate()
    {
        Debug.Log("Move input: " + MoveInput);
        // If speed is below max speed
        if (rb.linearVelocity.magnitude < MaxSpeed)
        {
            rb.AddForce(transform.up * MoveInput.y * Acceleration * Time.fixedDeltaTime);
        }

        // If 'A' or 'D' is pressed, then apply force to go horizontal
        // Only if the car is moving
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            float RotationAmount = MoveInput.x * SteeringSpeed * Time.fixedDeltaTime;

            if (MoveInput.y < 0) RotationAmount *= -1;
            rb.rotation -= RotationAmount;
        }

        // Handle Friction
        float forwardSpeed = Vector2.Dot(rb.linearVelocity, transform.up);
        float rightSpeed = Vector2.Dot(rb.linearVelocity, transform.right);

        // Dampen horizontal velocity
        Vector2 forwardVelocity = transform.up * forwardSpeed;
        Vector2 rightVelocity = transform.right * rightSpeed * Time.fixedDeltaTime;
        rb.linearVelocity = forwardVelocity + (rightVelocity * DriftFactor);
    }

    public Vector2 getSpeed()
    {
        return rb.totalForce;
    }
}
