using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudElemBasic : MonoBehaviour
{
    // Start is called before the first frame update

    public Text playerSpeed;
    private playerMovement2 pm;
    void Start()
    {
        pm = GetComponent<playerMovement2>();
        playerSpeed.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        playerSpeed.text = pm.movementSpeed.ToString();
    }
}
