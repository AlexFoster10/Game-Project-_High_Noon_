using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class powerCellScript : MonoBehaviour
{
    [Header("References")]
    public timer timerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 30f * Time.deltaTime,0f, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("COLLISION");
        timerScript.remainingTime += 3;
        this.gameObject.SetActive(false);
    }
}
