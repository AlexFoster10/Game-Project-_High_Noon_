using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class playerMovement2 : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement")]
    public float movementSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;
    public float slideSpeed;

    public float desiredMoveSpeed;
    public float lastDesiredMoveSpeed;

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
    public float playerHeight;
    public float standingPlayerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.C;



    public Transform orientation;
    public string playerSpeed;

    float horizontalInput;
    float verticalInput;

    

    public Vector3 movementDirection;

    public Rigidbody rb;
    CapsuleCollider playerCollider;

    public MovementState state;
    public enum MovementState 
    {
        walking,
        sprinting,
        crouching,
        sliding,
        air
    };

    public bool sliding;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
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
            //transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            playerHeight = standingPlayerHeight * crouchYScale;
            playerCollider.height = playerHeight;
            
            
            //rb.AddForce(Vector3.down * 5f, ForceMode.Force);
            playerHeight = standingPlayerHeight * crouchYScale;
        }
        bool check = (Time.timeScale == 0f);
        if (Input.GetKeyDown(crouchKey) && !(check))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - standingPlayerHeight*crouchYScale*0.5f, transform.position.z);
        }

        //crouch stop
        if (Input.GetKeyUp(crouchKey))
        {
            //transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            playerHeight = standingPlayerHeight;
            playerCollider.height = playerHeight;
        }

    }


    private void StateHandler()
    {

        if (sliding)
        {
            state = MovementState.sliding;

            if(OnSlope() && rb.velocity.y < 0.1f)
            {
                desiredMoveSpeed = slideSpeed;
            }
            else
            {
                desiredMoveSpeed = sprintSpeed;
            }
        }

        if (Input.GetKey(crouchKey) && grounded)
        {
            state = MovementState.crouching;
            desiredMoveSpeed = crouchSpeed * 0.5f;
        }

        if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;

        }

        else if (grounded && !sliding)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
        }

        else if(!grounded)
        {
            //print("air");
            state = MovementState.air;
            //desiredMoveSpeed = slideSpeed;
        }

        if(Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 3f && movementSpeed != 0)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLowerMoveSpeed());
        }

        else
        {
            movementSpeed = desiredMoveSpeed;
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
    }


    private IEnumerator SmoothlyLowerMoveSpeed()
    {
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - movementSpeed)*0.4f;
        float startVal = movementSpeed;
        while (time < difference) { 
            movementSpeed = Mathf.Lerp(startVal, desiredMoveSpeed, time/difference);
            time += Time.deltaTime;
            yield return null;
        }

        movementSpeed = desiredMoveSpeed;
        print(movementSpeed.ToString());
    }
    private void MovePlayer()
    {
        //calculate movement direction 
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;


        //on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDircetion(movementDirection) * movementSpeed * 20f , ForceMode.Force);
            rb.AddForce(Vector3.down * 80f, ForceMode.Force);
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

    public void Dash()
    {
        rb.AddForce(movementDirection.normalized * movementSpeed * 20f, ForceMode.Impulse);
    }



    private void SpeedControl()
    {

        if (OnSlope())
        {
            //print("ONSLOPE");
            if (rb.velocity.magnitude > movementSpeed)
            {
                rb.velocity = rb.velocity.normalized * movementSpeed;
            }
        }
        else
        {
            //print("Not Slope");
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if ((flatVel.magnitude > movementSpeed))
            {
                Vector3 limVel = flatVel.normalized * movementSpeed;
                rb.velocity = new Vector3(limVel.x, rb.velocity.y, limVel.z);
            }
        }
    }

    private void jump()
    {

        exitingSlope = true;
        //reset Y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }


    private void resetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }

    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.4f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            
            return angle < maxSlopeAngle && angle != 0;

        }

        return false;
    }

    public Vector3 GetSlopeMoveDircetion(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }
}

