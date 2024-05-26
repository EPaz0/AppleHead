using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    public float acceleration;
    public float groundSpeed;
    public float jumpSpeed;
    [Range(0f, 1f)]
    public float groundDecay;
    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public Transform FirePoint;

    public bool grounded;
    float xInput;
    float yInput;

    private bool m_FacingRight = true;  // For determining which way the player is currently facing.


    // Start is called before the first frame update
    void Start()
    {
        if (FirePoint == null)
        {
            Debug.LogError("FirePoint not assigned in the inspector!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        HandleJump();
    }

    void FixedUpdate(){
        CheckGround();
        ApplyFriction();
        MoveWithInput();

    }

    void GetInput(){
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }
    
    void MoveWithInput() {
        if (Mathf.Abs(xInput) > 0 ){
            // only apply velocity to the x axis, keep the current y velocity
            float increment = xInput * acceleration;
            // clamp function ensures that first parameters does not get updated the amount of times in the other parameters
            float newSpeed = Mathf.Clamp(body.velocity.x + increment, -groundSpeed, groundSpeed);
            // error!!! - the player wont stop dragging
            body.velocity = new Vector2(newSpeed, body.velocity.y);

            //FaceInput();

            if (xInput > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (xInput < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
    }

    void FaceInput()
    {
        float direction = Mathf.Sign(xInput);

        //float direction = Mathf.Sign(xInput);
        Vector3 scale = transform.localScale;
 
        if (scale.x != direction)
        {
            scale.x = direction;
            transform.localScale = scale;

            Debug.Log($"Player flipped. New localScale: {transform.localScale}");
        }
    }

    void HandleJump(){
        if (Input.GetButtonDown("Jump") && grounded){
            // keep the current x velocity and apply jumpspeed to y velocity
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }
    }


    void CheckGround() {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    void ApplyFriction(){
        if (grounded && xInput == 0 && body.velocity.y <= 0){
            body.velocity *= groundDecay;
        }
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
