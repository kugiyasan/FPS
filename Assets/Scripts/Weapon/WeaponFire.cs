using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponFire : MonoBehaviour
{
    public void Shoot(InputAction.CallbackContext ctx)
    {
        GunShoot weapon = gameObject.GetComponentInChildren<GunShoot>();
        weapon.Shoot(ctx);
    }
}