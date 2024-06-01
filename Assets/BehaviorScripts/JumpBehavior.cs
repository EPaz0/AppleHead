using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : StateMachineBehaviour
{
    private float timer;
    public float minTime;
    public float maxTime;

    private Transform playerPos;
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public float jumpInterval;
    private float jumpTimer;

    bool isJumping = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = animator.GetComponent<Rigidbody2D>();
        timer = Random.Range(minTime, maxTime);
        jumpTimer = jumpInterval;
        Debug.Log("Entered JumpBehavior state. Timer set to: " + timer);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("IdleTwo");
            Debug.Log("Timer expired. Transitioning to IdleTwo state.");
        }
        else
        {
            timer -= Time.deltaTime;
            Debug.Log("Timer: " + timer);
        }


        Vector2 target = new Vector2(playerPos.position.x, rb.position.y);
        Vector2 newPosition = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
        rb.MovePosition(newPosition);
        Debug.Log("Moving towards player. Current position: " + rb.position);

        // Handle jumping
        if (jumpTimer <= 0 && !isJumping)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            jumpTimer = jumpInterval; // Reset jump timer
            Debug.Log("Jumping with force: " + jumpForce);
        }
        else
        {
            jumpTimer -= Time.deltaTime;
        }


        // Check if the boss has landed
        if (isJumping && rb.velocity.y <= 0 && Mathf.Approximately(rb.velocity.y, 0))
        {
            isJumping = false; // Reset jumping state
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exited JumpBehavior state.");
    }
}
