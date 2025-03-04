using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamScript : MonoBehaviour
{

    public Transform PlayerCamPos;

    // Update is called once per frame
    void Update()
    {
        transform.position = PlayerCamPos.position;
    }
}
