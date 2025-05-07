using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class playerShoot : MonoBehaviour
{

    public GameObject gun;
    public gun gunScript;
    public levelCompletionCheck check;
    public GameObject timerParent;
    public timer timer;


    private void Start()
    {
        gunScript = gun.GetComponent<gun>();
        timer = timerParent.GetComponent<timer>();
    }
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && levelCompletionCheck.level1Check
            && (timer.remainingTime>0) )
        {
            gunScript.Shoot();
        }
    }
}
