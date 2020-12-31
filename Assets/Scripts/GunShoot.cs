using UnityEngine;
using UnityEngine.InputSystem;

public class GunShoot : MonoBehaviour
{
    [Header("Weapon Properties")]
    public Camera fpsCam;
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 5f;
    public float impactForce = 30f;
    // public bool fullAuto = false;
    // TODO fullAuto, semiAuto, burst
    // TODO shotgun(multiple rays), sniper(scope)

    [Header("Optional Effects")]
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (Time.time < nextTimeToFire)
            return;
        nextTimeToFire = Time.time + 1f / fireRate;

        if (muzzleFlash != null)
            muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
                target.TakeDamage(damage);

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * impactForce);

            if (impactEffect != null)
            {
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }
        }

    }
}
