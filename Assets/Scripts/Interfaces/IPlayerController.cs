using UnityEngine;

public interface IPlayerController
{
    void ProcessInput(Rigidbody rb, Transform transform, ref bool isGrounded);
    void ApplyMovement(Rigidbody rb, Transform transform);
}
