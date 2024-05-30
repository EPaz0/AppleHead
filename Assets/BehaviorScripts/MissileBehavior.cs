using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : StateMachineBehaviour
{

    public GameObject missilePrefab; // Assign the HomingMissile prefab in the Animator
    public float launchForce = 10f; // Adjust the launch force as needed
    public float missileDuration = 5f; // Duration for the missile state

    public float timer;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Initialize the timer
        timer = missileDuration;

        // Instantiate and launch the missile
        if (missilePrefab != null)
        {
            GameObject missile = Instantiate(missilePrefab, animator.transform.position, animator.transform.rotation);
            Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();

           /* if (rb != null)
            {
                rb.velocity = animator.transform.up * launchForce;
            }*/
        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            // Randomly choose the next state
            int nextState = Random.Range(0, 2); // 0 for Idle, 1 for MissleAttack

            if (nextState == 0)
            {
                animator.SetTrigger("Idle");
            }
            else
            {
                //animator.SetTrigger("MissleAttack");
                animator.SetTrigger("Throwing");
            }
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

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
