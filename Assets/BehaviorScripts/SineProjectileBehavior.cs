using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
public class SineProjectileBehavior : StateMachineBehaviour
{
    public GameObject projectilePrefab;
    public GameObject bossReference;
    public int  stateTimer = 10; // Duration the boss will throw projectiles
    public float moveSpeed = 5;
    private GameObject projectile;
    private float timer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Sine State");
        // Debug.Log("Number of time:" + stateTimer);
        // Initialize timers
        timer = stateTimer;

        bossReference = GameObject.Find("ShooterBanana");
        GameObject projectile = Instantiate(projectilePrefab, bossReference.transform.position, bossReference.transform.rotation);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Debug.Log("Timer: " + timer);
        timer -= Time.deltaTime;
        // Vector2 pos = projectile.transform.position;
        // pos.x -= moveSpeed * Time.fixedDeltaTime;
        // projectile.transform.position = pos;

        if (timer <= 0)
        {

            int rand = Random.Range(0, 3);
            if (rand == 0)
            {
                animator.SetTrigger("SinToIdle");
            }
            else if (rand == 1)
            {
                animator.SetTrigger("SinToJump");
            }
            else 
            {
                animator.SetTrigger("SinToCharge");
            }

            /*
             * else {
             * animator.SetTrigger("ChargeAttack);
             * }
             */
        }
  
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exit Sine State");
        animator.ResetTrigger("SineProjectile");
        animator.ResetTrigger("JumpToSin");
        animator.ResetTrigger("IdleToSin");
        animator.ResetTrigger("ChargeToSin");

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
