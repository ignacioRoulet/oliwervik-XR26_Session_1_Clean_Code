using UnityEngine;

public interface IPlayerController
{
    void ProcessInput(Rigidbody rb, Transform transform, ref bool isGrounded);
}
