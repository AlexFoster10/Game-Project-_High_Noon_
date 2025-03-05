using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovement2 : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement")]
    public float movementSpeed;

    public float groundDrag;


    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplyer;
    bool readyToJump;


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;



    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 movementDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        resetJump();

    }

    private void FixedUpdate() { 
        MovePlayer();
    }
    private void Update()
    {

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();

        //handledrag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            print("JUMP PRESSED");
            readyToJump = false;
            jump();
            Invoke(nameof(resetJump), jumpCooldown);
        }

    }


    private void MovePlayer()
    {
        //calculate movement direction 
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(movementDirection.normalized * movementSpeed * 10f, ForceMode.Force);
        }

        else if (!grounded)
        {
            rb.AddForce(movementDirection.normalized * movementSpeed * 10f * airMultiplyer, ForceMode.Force);
        }
    }

    private void jump()
    {
        //reset Y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }


    private void resetJump()
    {
        readyToJump = true;
    }

}
