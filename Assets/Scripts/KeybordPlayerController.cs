using UnityEngine;

public class KeyboardPlayerController : IPlayerController
{
    private readonly float moveSpeed;
    private readonly float jumpForce;
    private readonly float rotationSpeed;

    private float yaw;

    public KeyboardPlayerController(float moveSpeed, float jumpForce, float rotationSpeed)
    {
        this.moveSpeed = moveSpeed;
        this.jumpForce = jumpForce;
        this.rotationSpeed = rotationSpeed;
    }

    public void ProcessInput(Rigidbody rb, Transform transform, ref bool isGrounded)
    {
        // Rotation
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        yaw += mouseX;
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // Movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = transform.forward * v + transform.right * h;
        Vector3 velocity = direction.normalized * moveSpeed;

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}
