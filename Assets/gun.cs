using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private gunData gunData;
    [SerializeField] private Transform muzzle;
    [SerializeField] TextMeshProUGUI timerText;
    [Header("References")]
    public timer timerScript;

    float timeSinceLastShot;

    public GameObject passScreenUI;

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
                    if(hitInfo.transform.tag == "Target") {
                        Pass();
                    }
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

    public void Pass()
    {
        timerText.text = timerScript.remainingTime.ToString("0.00");
        passScreenUI.SetActive(true);
        Time.timeScale = 0f;
        //gameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


}
