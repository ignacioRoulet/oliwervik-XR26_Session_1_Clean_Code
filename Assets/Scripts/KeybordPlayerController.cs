using UnityEngine;

public class KeyboardPlayerController : IPlayerController
{
    private readonly float moveSpeed;
    private readonly float jumpForce;
    private readonly float rotationSpeed;

    private float yaw;
    private float hInput;
    private float vInput;
    private bool jumpRequested;

    public KeyboardPlayerController(float moveSpeed, float jumpForce, float rotationSpeed)
    {
        this.moveSpeed = moveSpeed;
        this.jumpForce = jumpForce;
        this.rotationSpeed = rotationSpeed;
    }

    public void ProcessInput(Rigidbody rb, Transform transform, ref bool isGrounded)
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        yaw += mouseX;
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);

        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    public void ApplyMovement(Rigidbody rb, Transform transform)
    {
        Vector3 direction = transform.forward * vInput + transform.right * hInput;
        Vector3 velocity = direction.normalized * moveSpeed;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}

