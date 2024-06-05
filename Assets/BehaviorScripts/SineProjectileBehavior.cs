using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
public class SineProjectileBehavior : StateMachineBehaviour
{
    public GameObject projectilePrefab;
    public GameObject bossReference;
    public float stateTimer = 10f; // Duration the boss will throw projectiles
    public float moveSpeed = 5;
    private GameObject projectile;
    private float timer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Initialize timers
        timer = stateTimer;
        projectile = Instantiate(projectilePrefab, bossReference.transform.position, bossReference.transform.rotation);
        // Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        // Vector2 pos = projectile.transform.position;
        // pos.x -= moveSpeed * Time.fixedDeltaTime;
        // projectile.transform.position = pos;

        if (timer <= 0)
        {

            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                animator.SetTrigger("ChargeAttack");
            }
            else
            {
                animator.SetTrigger("JumpAttack");
            }
        }
        else
        {
            timer -= Time.deltaTime;
            // Debug.Log("Timer: " + timer);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
