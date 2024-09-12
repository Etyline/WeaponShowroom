using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float Range = 100f;
    [SerializeField] float damage = 20f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] WwiseShot Shot;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] float timeBetweenShots = 0.8f;

    bool canShoot = true;

    public bool allowButtonHold;

    void Update()
    {
        if (allowButtonHold == false)
        {
            if (Input.GetMouseButtonDown(0) && canShoot == true)
            {
                StartCoroutine(Shoot());
                Debug.Log("ShotFired");
                Shot.Shot();
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && canShoot == true)
            {
                StartCoroutine(Shoot());
                Debug.Log("ShotFired");
                Shot.Shot();
            }
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo() > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo();
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, Range))
        {
            Debug.Log("I hit: " + hit.transform.name);
            CreateHitImpact(hit);
            Target target = hit.transform.GetComponent<Target>();
            if (target == null) return;
            target.TakeDmg(damage);
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1);
    }
}
