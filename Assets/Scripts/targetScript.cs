using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetScript : MonoBehaviour
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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("COLLISION");
        //timerScript.LevelPassed();
        //levelCompletionCheck.level1Check = true;
    }
}
