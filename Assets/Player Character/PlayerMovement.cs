using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration;
    public float groundSpeed;
    public float jumpSpeed;
    [Range(0f, 1f)]
    public float groundDecay;
    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public Transform FirePoint;
    public GameObject projectilePrefab; // Add a reference to the projectile prefab
    
    public bool grounded;
    float xInput;
    float yInput;

    Animator animator;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    // The original local position and rotation of the FirePoint
    private Vector2 originalFirePointPosition;
    private Quaternion originalFirePointRotation;

    // The position for FirePoint when aiming up
    private Vector2 firePointUpPosition = new Vector2(0.44f, 1.93f); // Exact position
    private Quaternion firePointUpRotation = Quaternion.Euler(0, 0, 89.869f); // Exact rotation

    // The position for FirePoint when aiming diagonally up-right
    private Vector2 firePointDiagonalRightPosition = new Vector2(1.62f, 0.89f); // Adjust the position as needed
    private Quaternion firePointDiagonalRightRotation = Quaternion.Euler(0, 0, 35.889f); // Adjust the rotation as needed

    private Vector2 firePointDiagonalLeftPosition = new Vector2(-1.62f, 0.89f); // Adjust the position as needed
    private Quaternion firePointDiagonalLeftRotation = Quaternion.Euler(0, 0, 144.111f); // Adjust the rotation as needed

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (FirePoint == null)
        {
            Debug.LogError("FirePoint not assigned in the inspector!");
        }
        else
        {
            originalFirePointPosition = FirePoint.localPosition;
            originalFirePointRotation = FirePoint.localRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        HandleJump();
        HandleShooting();
    }

    void FixedUpdate()
    {

         CheckGround();
        ApplyFriction();
        if (!Input.GetKey(KeyCode.LeftShift)) // Prevent movement when holding Shift
        {
            MoveWithInput();
        }
        else
        {
            // Allow turning around while holding Shift
            HandleTurning();
        }
    }
   void HandleTurning()
    {
        if (xInput > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (xInput < 0 && m_FacingRight)
        {
            Flip();
        }
    }
    void GetInput()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    void MoveWithInput()
    {
        if (Mathf.Abs(xInput) > 0)
        {
            // only apply velocity to the x axis, keep the current y velocity
            float increment = xInput * acceleration;
            // clamp function ensures that first parameters does not get updated the amount of times in the other parameters
            float newSpeed = Mathf.Clamp(body.velocity.x + increment, -groundSpeed, groundSpeed);
            body.velocity = new Vector2(newSpeed, body.velocity.y);

            if (xInput > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (xInput < 0 && m_FacingRight)
            {
                Flip();
            }

        }
        animator.SetFloat("xVelocity", Math.Abs(body.velocity.x));
        animator.SetFloat("yVelocity", body.velocity.y);


    }

    void HandleJump()
    {
        // animator.SetBool("isJumping", !isGrounded);
        if (Input.GetButtonDown("Jump") && grounded)
        {
            // keep the current x velocity and apply jumpspeed to y velocity
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            grounded = false;
            animator.SetBool("isJumping", !grounded);
        }
        else if (grounded){
            grounded = true;
            animator.SetBool("isJumping", !grounded);
        }
        else {
            animator.SetBool("isJumping", !grounded);
        }

    }

    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    void ApplyFriction()
    {
        if (grounded && xInput == 0 && body.velocity.y <= 0)
        {
            body.velocity *= groundDecay;
        }
    }

    void HandleShooting()
    {
        //Debug.Log("HandleShooting called");
        //Debug.Log("m_FacingRight: " + m_FacingRight);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                // Diagonal up-right
                FirePoint.localPosition = firePointDiagonalRightPosition;
                FirePoint.localRotation = firePointDiagonalRightRotation;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                // Diagonal up-left
                if (m_FacingRight)
                {
                    FirePoint.localPosition = new Vector2(-firePointDiagonalRightPosition.x, firePointDiagonalRightPosition.y);
                    FirePoint.localRotation = Quaternion.Euler(0, 0, 144.111f);
                }
                else
                {
                    FirePoint.localPosition = new Vector2(firePointDiagonalRightPosition.x, firePointDiagonalRightPosition.y);
                    FirePoint.localRotation = Quaternion.Euler(0, 0, 35.889f);
                }
            }
            else
            {
                // Straight up
                FirePoint.localPosition = firePointUpPosition;
                FirePoint.localRotation = firePointUpRotation;
            }

            Shoot();
        }
        else
        {
            FirePoint.localPosition = originalFirePointPosition; // Reset FirePoint position
            FirePoint.localRotation = originalFirePointRotation; // Reset FirePoint rotation
        }
        //Debug.Log("FirePoint Position: " + FirePoint.localPosition);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Rotate the player
        transform.Rotate(0f, 180f, 0f);

        // Adjust FirePoint's position based on the new facing direction
        if (m_FacingRight)
        {
            FirePoint.localPosition = originalFirePointPosition;
        }
        else
        {
            FirePoint.localPosition = new Vector2(-originalFirePointPosition.x, originalFirePointPosition.y);
        }
        //Debug.Log("Flip called, m_FacingRight: " + m_FacingRight);
        //Debug.Log("FirePoint Position after Flip: " + FirePoint.localPosition);
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1")) // Assuming Fire1 is set up in the Input settings
        {
            GameObject projectile = Instantiate(projectilePrefab, FirePoint.position, FirePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = FirePoint.up * 10f; // Adjust the speed as needed
        }
    }

    // private void OnTriggerEnter2D(Collider2D collision){
    //     // grounded = true;
    //     // animator.SetBool("isJumping", !grounded);

    // }
}