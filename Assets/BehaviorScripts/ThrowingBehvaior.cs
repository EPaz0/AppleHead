using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingBehavior : StateMachineBehaviour
{

    public GameObject projectile; // Assign the BananaPeel prefab in the Animator
    public float launchForce = 5f; // Adjust the launch force as needed

    private float timer;
    public float duration = 2f; // Duration for the throwing state



    // Specified positions for the banana peels
    private Vector3[] spawnPositions = new Vector3[]
    {
        new Vector3(10.02f, 9.7f, 0f),
        new Vector3(-10.11f, 10.59f, 0f),
        new Vector3(-0.58f, 6.37f, 0f)
    };

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = duration;

        if (projectile != null)
        {
            foreach (var position in spawnPositions)
            {
                GameObject thrownProjectile = Instantiate(projectile, position, Quaternion.identity);
                Rigidbody2D rb = thrownProjectile.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    // Apply an initial launch force if needed
                    rb.velocity = Vector2.down * launchForce; // Gravity will pull it down
                }
            }
        }

        //Old Thrower
        /*
        // Instantiate and launch projectiles
        if (projectile != null)
        {
            GameObject thrownProjectile1 = Instantiate(projectile, animator.transform.position, animator.transform.rotation);
            GameObject thrownProjectile2 = Instantiate(projectile, animator.transform.position, animator.transform.rotation);

            Rigidbody2D rb1 = thrownProjectile1.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = thrownProjectile2.GetComponent<Rigidbody2D>();

            if (rb1 != null)
            {
                rb1.velocity = animator.transform.right * launchForce;
            }
            if (rb2 != null)
            {
                rb2.velocity = -animator.transform.right * launchForce;
            }
        }*/
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            // Randomly choose the next state
            int nextState = Random.Range(0, 3); // 0 for Idle, 1 for MissleAttack

            if (nextState == 0)
            {
                animator.SetTrigger("MissileAttack");
            }
            else
            {
                animator.SetTrigger("Idle");
               // animator.SetTrigger("Idle");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Reset triggers and bools to avoid re-entering the same state
        //animator.ResetTrigger("Throwing");
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
