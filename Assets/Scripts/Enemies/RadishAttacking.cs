using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadishAttacking : StateMachineBehaviour
{
    RadishBehavior radishBehavior;
    GameObject player;
    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        radishBehavior = animator.GetComponent<RadishBehavior>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = animator.GetComponent<Rigidbody2D>();
        animator.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player != null)
        {
            Vector2 direction = ((Vector2)player.transform.position - (Vector2)animator.transform.position).normalized;
            rb.velocity = direction * radishBehavior.moveSpeed;

            if (Vector2.Distance(player.transform.position, animator.transform.position) < radishBehavior.distanceToEndAttack)
            {
                animator.SetTrigger("End Attack");
            }
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
            rb.velocity = Vector2.zero;
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
