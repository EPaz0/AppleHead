using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBehavior : StateMachineBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f; // Force of the jump

    private Transform player;
    private Rigidbody2D rb;
    private bool hasJumped = false;
    private bool isWaiting = false;
    private Vector2 chargeDirection;
    private float waitTime = 2f; // Wait time before charging
    private float waitTimer;
    private int chargeCount;
    private int maxCharges;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Find the player in the scene
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the Rigidbody2D component
        rb = animator.GetComponent<Rigidbody2D>();

        hasJumped = false; // Reset the jumped flag
        isWaiting = false; // Reset the waiting flag
        waitTimer = waitTime; // Reset the wait timer

        // Initialize charge count
        chargeCount = 0;
        maxCharges = Random.Range(1, 4); // Randomize charge count between 1 and 3

        // Perform a small jump to signal the charge
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        //Debug.Log("Entered ChargeBehavior state. Will charge " + maxCharges + " times.");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // Debug.Log("OnStateUpdate: chargeCount = " + chargeCount + ", maxCharges = " + maxCharges + ", velocity = " + rb.velocity);

        if (chargeCount >= maxCharges && rb.velocity == Vector2.zero)
        {
            int nextState = Random.Range(0, 3);

            if (nextState == 0)
            {
                animator.SetTrigger("IdleTwo");
            }
            else if (nextState == 1 )
            {
                animator.SetTrigger("JumpAttack");
            }
            else {
                animator.SetTrigger("SineProjectile");
            }

           // Debug.Log("Completed charging. Transitioning to next state.");
        }
        else
        {
            if (!hasJumped && rb.velocity.y <= 0.1f)
            {
                // Once the jump is completed, start waiting
                hasJumped = true;
                isWaiting = true;
                //Debug.Log("Jump completed. Starting wait timer.");
            }

            if (isWaiting)
            {
                waitTimer -= Time.deltaTime;
               //Debug.Log("Waiting... waitTimer = " + waitTimer);

                if (waitTimer <= 0)
                {
                    isWaiting = false;
                    // Recalculate the direction to charge towards
                    chargeDirection = new Vector2(player.position.x - animator.transform.position.x, 0).normalized;
                    rb.velocity = chargeDirection * speed; // Start charging
                    chargeCount++; // Increment charge count
                    waitTimer = waitTime; // Reset the wait timer for the next charge

                    //Debug.Log("Charging towards player. Charge count: " + chargeCount);
                }
            }

            if (rb.velocity == Vector2.zero && hasJumped)
            {
                // Reset jump flag for the next charge
                hasJumped = false;
               // Debug.Log("Charge stopped. Preparing for the next charge.");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Stop moving when exiting the state
        rb.velocity = Vector2.zero;
       // Debug.Log("Exited ChargeBehavior state.");

    }
}