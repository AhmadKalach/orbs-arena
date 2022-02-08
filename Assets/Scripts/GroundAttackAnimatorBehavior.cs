using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAttackAnimatorBehavior : StateMachineBehaviour
{
    public GameObject toSpawn;
    public AudioClip sfx;
    public float timeBetweenAttacks;
    public float timeToIdle;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    float startTime;
    float lastAttackTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        startTime = Time.time;
        lastAttackTime = Time.time;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.time > startTime + timeToIdle)
        {
            animator.SetTrigger("Idle");
        }

        if (Time.time > lastAttackTime + timeBetweenAttacks)
        {
            lastAttackTime = Time.time;
            Attack();
        }
    }

    void Attack()
    {
        GameObject instance = Instantiate(toSpawn);
        Vector2 randomPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        instance.transform.position = randomPos;
        AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position);
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
