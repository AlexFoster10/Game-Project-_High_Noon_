using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovement2 : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement")]
    public float movementSpeed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 movementDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    private void FixedUpdate() { 
        MovePlayer();
    }
    private void Update()
    {
        MyInput();

    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        

    }

    private void MovePlayer()
    {
        //calculate movement direction 
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        

        rb.AddForce(movementDirection.normalized * movementSpeed * 10f, ForceMode.Force);
    }
}
