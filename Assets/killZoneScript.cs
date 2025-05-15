using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killZoneScript : MonoBehaviour
{


    [Header("References")]
    public timer timerScript;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            timerScript.LevelFailed();
        }
        

    }
}
