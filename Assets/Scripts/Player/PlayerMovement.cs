using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float groundCheckDistance = 1.1f;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 100f;
    public Transform cameraHolder; // Assign in inspector (CameraHolder)

    private Rigidbody rb;
    private float xRotation = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Freeze rotation so we don't fall over
        rb.freezeRotation = true;

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMouseLook();
        HandleJump();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Direction relative to player's facing
        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;
        Vector3 targetVelocity = moveDirection.normalized * moveSpeed;

        // Preserve existing Y velocity (falling, jumping)
        Vector3 velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);
        rb.velocity = velocity;
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        // Simple raycast to check ground contact
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }
}
