using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCrouch : MonoBehaviour
{
    public Transform playerBody;
    public CharacterController controller;

    bool isCrounching = false;

    public void Crouch(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) return;

        isCrounching = ctx.started;
        controller.height *= isCrounching ? 0.5f : 2f;
        Vector3 scale = playerBody.localScale;
        scale.y *= isCrounching ? 0.5f : 2f;
        playerBody.localScale = scale;
    }
}