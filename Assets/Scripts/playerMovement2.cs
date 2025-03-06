using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovement2 : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement")]
    private float movementSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplyer;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;


    [Header("Ground Check")]
    private float playerHeight;
    public float standingPlayerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;



    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 movementDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState 
    {
        walking,
        sprinting,
        crouching,
        air
    };

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        playerHeight = standingPlayerHeight;

        startYScale = transform.localScale.y;

    }

    private void FixedUpdate() { 
        MovePlayer();
    }
    private void Update()
    {
        if (OnSlope())
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight, whatIsGround);
        }

        else {
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        }
        MyInput();
        SpeedControl();
        StateHandler();

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
        

        //jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            jump();
            Invoke(nameof(resetJump), jumpCooldown);
        }

        //crouch start
        if (Input.GetKey(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            
            //rb.AddForce(Vector3.down * 5f, ForceMode.Force);
            playerHeight = standingPlayerHeight * crouchYScale;
        }

        if (Input.GetKeyDown(crouchKey))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - standingPlayerHeight*crouchYScale*0.5f, transform.position.z);
        }

        //crouch stop
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            playerHeight = standingPlayerHeight;
        }

    }


    private void StateHandler()
    {

        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            movementSpeed = crouchSpeed * 0.5f;
        }

        if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            movementSpeed = sprintSpeed;
        }

        else if (grounded)
        {
            state = MovementState.walking;
            movementSpeed = walkSpeed;
        }

        else
        {
            state = MovementState.air;
        }
    }
    private void MovePlayer()
    {
        //calculate movement direction 
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;


        //on slope
        if (OnSlope())
        {
            rb.AddForce(GetSlopeMoveDircetion() * movementSpeed * 1.4f , ForceMode.Force);
            rb.AddForce(Vector3.down * 10f, ForceMode.Force);
        }

        //on ground
        if (grounded)
        {
            rb.AddForce(movementDirection.normalized * movementSpeed * 10f, ForceMode.Force);
        }

        //in air
        else if (!grounded)
        {
            rb.AddForce(movementDirection.normalized * movementSpeed * 10f * airMultiplyer, ForceMode.Force);
        }

        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if((flatVel.magnitude > movementSpeed) && grounded)
        {
            Vector3 limVel = flatVel.normalized * movementSpeed;
            rb.velocity = new Vector3(limVel.x, rb.velocity.y, limVel.z);   
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

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.4f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            print("ONSLOPE");
            return angle < maxSlopeAngle && angle != 0;

        }

        return false;
    }

    private Vector3 GetSlopeMoveDircetion()
    {
        return Vector3.ProjectOnPlane(movementDirection, slopeHit.normal).normalized;
    }
}
