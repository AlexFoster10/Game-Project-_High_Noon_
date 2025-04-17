using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerShoot : MonoBehaviour
{

    public GameObject gun;
    public gun gunScript;

    private void Start()
    {
        gunScript = gun.GetComponent<gun>();
    }
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gunScript.Shoot();
        }
    }
}
