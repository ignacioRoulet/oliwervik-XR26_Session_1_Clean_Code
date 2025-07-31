using UnityEngine;

public class KeyboardPlayerController : IPlayerController
{
    private readonly float moveSpeed;
    private readonly float rotationSpeed;
    private float yaw;

    public KeyboardPlayerController(float moveSpeed, float rotationSpeed)
    {
        this.moveSpeed = moveSpeed;
        this.rotationSpeed = rotationSpeed;
    }



    public void ApplyMovement(Rigidbody rb)
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = rb.transform.forward * v + rb.transform.right * h;
        Vector3 velocity = direction.normalized * moveSpeed;

        Vector3 newPos = rb.position + velocity * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }

    public void ApplyRotation(Transform transform)
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        yaw += mouseX;
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }

}