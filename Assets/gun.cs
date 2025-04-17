using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private gunData gunData;
    [SerializeField] private Transform muzzle;

    float timeSinceLastShot;

    private void Start()
    {
        //playerShoot.shootInput += Shoot;
        gunData.currentAmmo = 1;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / gunData.fireRate / 60;

    public void Shoot()
    {
        if(gunData.currentAmmo > 0)
        {
             if (CanShoot())
            {
                if(Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxD)) {

                    Debug.Log(hitInfo.transform.name);
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0f;
                OnGunShot();
            }
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        Debug.DrawRay(muzzle.position, muzzle.forward);
    }


    private void OnGunShot()
    {
        //not implemented
    }


}
