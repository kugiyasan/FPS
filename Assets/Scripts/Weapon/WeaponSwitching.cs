using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;

    public void ChangeWeapon(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            float value = ctx.ReadValue<float>();
            int v = (int)Mathf.Clamp(value, -1, 1);
            selectedWeapon = (selectedWeapon - v) % transform.childCount;
            if (selectedWeapon < 0)
                selectedWeapon += transform.childCount;
            SelectWeapon();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);

            i++;
        }
    }
}
