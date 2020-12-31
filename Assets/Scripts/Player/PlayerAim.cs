using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    public Camera cam;
    public float zoomRatio = 2f;

    bool isAiming = false;

    public void Aim(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) return;

        isAiming = ctx.started;
        cam.fieldOfView *= isAiming ? 1 / zoomRatio : zoomRatio;
    }
}
