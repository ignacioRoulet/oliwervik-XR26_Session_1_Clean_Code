using UnityEngine;

public interface IPlayerController
{
    void ApplyMovement(Rigidbody rb);
    void ApplyRotation(Transform transform);
}
