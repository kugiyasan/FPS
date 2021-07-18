using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponEventRouting : MonoBehaviour
{
    public void Shoot(InputAction.CallbackContext ctx)
    {
        WeaponFire weapon = gameObject.GetComponentInChildren<WeaponFire>();
        weapon.Shoot(ctx);
    }

    public void Aim(InputAction.CallbackContext ctx)
    {
        WeaponAim weapon = gameObject.GetComponentInChildren<WeaponAim>();
        weapon.Aim(ctx);
    }
}