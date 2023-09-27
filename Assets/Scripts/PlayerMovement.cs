using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalMovement;
    private float verticalMovement;

    private float maxSpeed = 150f;
    private float timeToMaxHSpeed = 0.1f;
    private float hSpeed;
    private float zSpeed;
    private float vSpeed;
    private float maxVSpeed = 9f;

    private float accelRate;
    private float decelRate;

    public bool grounded;
    private float gravity = 0.2f;
    public bool gravityInverted = false;
    private float fallDistanceCheck = 0.1f;
    public Transform groundedChecker;
    private LayerMask groundLayer;

    private Vector3 finalMovement;

    public Rigidbody playerRB;


    void Start()
    {
        playerRB = GetComponent<Rigidbody>();

        accelRate = maxSpeed / timeToMaxHSpeed; // Acceleration = MaxSpeed / Time to max speed
        decelRate = maxSpeed / (timeToMaxHSpeed / 2); // Deceleration = MaxSpeed / Time to max speed, but 2 times as fast, so the time is divided by 2

        groundLayer = 1 << 11;
    }

    void Update()
    {
        // 1: Read Input
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical"); 
    }

    private void FixedUpdate()
    {
        // 1: Calculate Velocity function to add damping to movement + absolute speed calculation for diagonal movement
        CalculateVelocity(horizontalMovement, verticalMovement);

        // 2: Calculate vertical speed
        CalculateGravity();

        // 3: Move player
        MovePlayer(finalMovement);

    }

    private void CalculateVelocity(float hMov, float zMov)
    {
        if (hMov > 0)
        {
             hSpeed = Mathf.Clamp((hSpeed + accelRate * Time.deltaTime), -maxSpeed, maxSpeed);
        }

        else if (hMov == 0)
        {
            if (hSpeed > 0)
            {
                hSpeed = Mathf.Clamp((hSpeed - decelRate * Time.deltaTime), 0, maxSpeed);
            }
            if (hSpeed < 0)
            {
                hSpeed = Mathf.Clamp((hSpeed + decelRate * Time.deltaTime), -maxSpeed, 0);
            }
        }

        else if (hMov < 0)
        {
            hSpeed = Mathf.Clamp((hSpeed - accelRate * Time.deltaTime), -maxSpeed, maxSpeed);
        }


        if (zMov > 0)
        {
            zSpeed = Mathf.Clamp((zSpeed + accelRate * Time.deltaTime), -maxSpeed, maxSpeed);
        }

        else if (zMov == 0)
        {
            if (zSpeed > 0)
            {
                zSpeed = Mathf.Clamp((zSpeed - decelRate * Time.deltaTime), 0, maxSpeed);
            }
            if (zSpeed < 0)
            {
                zSpeed = Mathf.Clamp((zSpeed + decelRate * Time.deltaTime), -maxSpeed, 0);
            }
        }

        else if (zMov < 0)
        {
            zSpeed = Mathf.Clamp((zSpeed - accelRate * Time.deltaTime), -maxSpeed, maxSpeed);
        }

        // Absolute velocity
        if (hSpeed != 0 && zSpeed != 0)
        {
            hSpeed *= 0.707f; 
            zSpeed *= 0.707f;
        }

        Vector3 xzPlaneMovement = transform.right * hSpeed + transform.forward * zSpeed;

        finalMovement = new Vector3(xzPlaneMovement.x, vSpeed, xzPlaneMovement.z);
    }

    private void CalculateGravity()
    {

        if (!gravityInverted)
        {
            if (Physics.Raycast(groundedChecker.position, Vector3.down, fallDistanceCheck, groundLayer))
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }

            if (!grounded)
            {
                vSpeed += gravity;
            }
            else
            {
                vSpeed = 0;
            }

            if (vSpeed > maxVSpeed)
            {
                vSpeed = maxVSpeed;
            }

            playerRB.AddForce(Vector3.down * vSpeed, ForceMode.VelocityChange);
        }
        else
        {
            if (Physics.Raycast(groundedChecker.position, Vector3.up, fallDistanceCheck, groundLayer))
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }

            if (!grounded)
            {
                vSpeed -= gravity;
            }
            else
            {
                vSpeed = 0;
            }

            if (vSpeed < -maxVSpeed)
            {
                vSpeed = -maxVSpeed;
            }

            playerRB.AddForce(Vector3.down * vSpeed, ForceMode.VelocityChange);
            
        }
    }

    private void MovePlayer(Vector3 movement)
    {
        playerRB.velocity = finalMovement * Time.deltaTime; // Only works with dynamic rigidbody
    }
}
