using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerTrackingScript : MonoBehaviour
{


    Transform mainCam;
    Transform unit;
    Transform worldSpaceCanvas;

    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main.transform;
        unit = transform.parent;
        worldSpaceCanvas = GameObject.FindGameObjectWithTag("WatchTimerCanvas").transform;

        transform.SetParent(worldSpaceCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.LookAt(transform.position + mainCam.transform.rotation * Vector3.forward,
        mainCam.transform.rotation * Vector3.up);
        transform.position = unit.position + offset;

    }
}
