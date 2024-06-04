using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : StateMachineBehaviour
{

    public GameObject missilePrefab; // Assign the HomingMissile prefab in the Animator
    public float launchForce = 10f; // Adjust the launch force as needed
    public float missileDuration = 5f; // Duration for the missile state

    public float attackTimer;

    private float timer;
    public float minTime;
    public float maxTime;
    // Define positions for missile spawning
    public Vector3[] spawnPositions = new Vector3[] {
        new Vector3(16.84f, 12.62f, -0.05745613f),
        new Vector3(-18.96f, 12.62f, -0.05745613f),
        new Vector3(0.51f,18.17f,-0.05745613f)
    };

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Initialize the timers
        timer = missileDuration;
        attackTimer = 2f; // Set the initial attack timer to 2 seconds

        LaunchMissile();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            LaunchMissile();
            attackTimer = 2f; // Reset the attack timer to 2 seconds
        }

        if (timer <= 0)
        {
            // Randomly choose the next state
            int nextState = Random.Range(0, 3); // 0 for Idle, 1 for MissleAttack

            if (nextState == 0)
            {
                animator.SetTrigger("Throwing");
            }
            else
            {
                animator.SetTrigger("Idle");
            }
        }
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    private void LaunchMissile()
    {
        if (missilePrefab != null)
        {
            // Select a random position from spawnPositions
            Vector3 randomPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];

            GameObject missile = Instantiate(missilePrefab, randomPosition, Quaternion.identity);
            Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();

            /* if (rb != null)
            {
                rb.velocity = animator.transform.up * launchForce;
            }*/
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
