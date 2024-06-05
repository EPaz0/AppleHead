using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : StateMachineBehaviour
{
    public float speed;
    public float jumpForce;
    public float jumpInterval;
    private float jumpTimer;

    private Transform playerPos;
    private Rigidbody2D rb;
    private int jumpCount;
    private int maxJumps;
    private bool isJumping;

    // Add ground checking variables
    private LayerMask groundLayer;
    private Transform groundCheck;
    private float groundCheckRadius = 0.1f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = animator.GetComponent<Rigidbody2D>();
        groundCheck = animator.transform.Find("GroundCheck"); // Assuming there is an empty GameObject named GroundCheck as a child of the boss
        groundLayer = LayerMask.GetMask("Ground");

        jumpTimer = 0; // Start jumping immediately
        jumpCount = 0;
        maxJumps = Random.Range(4, 7); // Randomize jump count between 4 and 6
        isJumping = false;

        Debug.Log("Entered JumpBehavior state. Will jump " + maxJumps + " times.");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (jumpCount >= maxJumps && IsGrounded())
        {
            int nextState = Random.Range(0, 3);

            if (nextState == 0)
            {
                animator.SetTrigger("IdleTwo");
            }
            else
            {
                animator.SetTrigger("ChargeAttack");
            }

            Debug.Log("Completed jumping and grounded. Transitioning to next state.");
        }
        else
        {
            if (jumpTimer <= 0 && !isJumping && IsGrounded())
            {
                Vector2 target = new Vector2(playerPos.position.x, rb.position.y);
                Vector2 jumpDirection = (target - rb.position).normalized * speed;
                rb.AddForce(new Vector2(jumpDirection.x, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
                jumpTimer = jumpInterval; // Reset jump timer

                Debug.Log("Jumping towards player with force: " + jumpForce + " and direction: " + jumpDirection);
            }
            else
            {
                jumpTimer -= Time.deltaTime;
            }

            // Check if the boss has landed
            if (isJumping && Mathf.Abs(rb.velocity.y) < 0.1f && IsGrounded())
            {
                isJumping = false; // Reset jumping state
                jumpCount++; // Increment jump count
                Debug.Log("Landed. Jump count: " + jumpCount);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exited JumpBehavior state.");
    }

    // Check if the boss is on the ground
    private bool IsGrounded()
    {
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        Debug.Log("IsGrounded check: " + grounded);
        return grounded;
    }
}