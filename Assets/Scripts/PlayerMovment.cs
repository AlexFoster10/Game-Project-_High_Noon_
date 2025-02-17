using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterController controller;

    public float playerSpeed = 12.0f;
    public float playerGravity = 9.81f * 2f;
    public float playerJumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool grounded;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (grounded && velocity.y < 0 )
        {
            velocity.y = 2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * playerSpeed * Time.deltaTime);


        if (Input.GetButtonDown("Jump")&& grounded)
        {
            velocity.y = Mathf.Sqrt(playerJumpHeight * 2f * playerGravity);

        }

        velocity.y = playerGravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
