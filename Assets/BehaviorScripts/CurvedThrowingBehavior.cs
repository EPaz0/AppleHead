using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
public class CurvedThrowingBehavior : StateMachineBehaviour
{
    public GameObject projectilePrefab;
    public GameObject bossReference;
    public float stateTimer = 5f; // Duration the boss will throw projectiles
    
    public float moveSpeed = 5;
    

    private float timer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Initialize timers
        timer = stateTimer;
        spawnProjectile();
        
        
    
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;

        GameObject projectile = Instantiate(projectilePrefab, bossReference.transform.position, bossReference.transform.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Vector2 pos = projectile.transform.position;
        pos.x -=moveSpeed * Time.fixedDeltaTime;


        if (timer <= 0)
        {

            int rand = Random.Range(0, 1);
            if (rand == 0)
            {
                animator.SetTrigger("IdleTwo");
            }
            else
            {
                animator.SetTrigger("JumpAttack");
            }
        }
        else
        {
            timer -= Time.deltaTime;
            Debug.Log("Timer: " + timer);
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

     private void spawnProjectile()
    {
        if (projectilePrefab != null)
        {
            // Select a random position from spawnPositions

            // GameObject projectile = Instantiate(projectilePrefab, bossReference.transform.position, bossReference.transform.rotation);
            // Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            // Vector2 pos = projectile.transform.position;

        }
    }
}
