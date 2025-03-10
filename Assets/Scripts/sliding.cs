using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliding : MonoBehaviour
{

    [Header("References")]
    public Transform orientation;
    public Transform playerObject;
    private Rigidbody rb;
    private playerMovement2 pm;

    [Header("Sliding")]
    public float maxSlideDuration;
    public float slideForce;
    private float slideTimer;

    public float slideYScale;
    private float startYScale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<playerMovement2>();

        startYScale = playerObject.localScale.y;

    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0)) {
            StartSlide();
        }
        if (Input.GetKey(slideKey))
        {
            playerObject.localScale = new Vector3(playerObject.localScale.x, slideYScale, playerObject.localScale.z);
            pm.playerHeight = pm.standingPlayerHeight * slideYScale * 0.8f;
        }

        if (Input.GetKeyUp(slideKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            pm.playerHeight = pm.standingPlayerHeight;
        }

        if (Input.GetKeyUp(slideKey) && pm.sliding)
        {
            StopSlide();
        }

    }

    private void StartSlide() {
        pm.sliding = true;
   
        
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideDuration;
    }

    private void FixedUpdate()
    {
        if (pm.sliding)
        {
            slidingMovement();
        }
    }

    private void slidingMovement()
    {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if (!pm.OnSlope()  || rb.velocity.y > -0.1f )
        {
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);
            slideTimer -= Time.deltaTime;
        }
        

        else
        {
            rb.AddForce(pm.GetSlopeMoveDircetion(inputDirection.normalized) * slideForce, ForceMode.Force);
           //slideTimer -= Time.deltaTime;
        }

        if (slideTimer <= 0)
        {
            StopSlide();
        }
    }

    private void StopSlide()
    {
        pm.playerHeight = pm.standingPlayerHeight;
        pm.sliding = false;
        //playerObject.localScale = new Vector3(playerObject.localScale.x, startYScale, playerObject.localScale.z);
    }
}

