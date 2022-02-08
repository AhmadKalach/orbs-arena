using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAttackBehavior : StateMachineBehaviour
{
    public GameObject fireball;
    public float fireballSpeed;
    public float timeBetweenAttacks;
    public float timeToIdle;
    public AudioClip sfx;

    GameObject handPos;
    float lastFireballTime;
    float startTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        startTime = Time.time;
        lastFireballTime = Time.time;
        handPos = animator.GetComponent<BossBehavior>().handPos;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.time > lastFireballTime + timeBetweenAttacks)
        {
            lastFireballTime = Time.time;
            Fire();
        }

        if (Time.time > startTime + timeToIdle)
        {
            animator.SetTrigger("Idle");
        }
    }

    void Fire()
    {
        float angle = Random.Range(0, 360);
        GameObject instance = Instantiate(fireball);
        instance.transform.position = handPos.transform.position;
        instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        instance.GetComponent<Rigidbody2D>().velocity = instance.transform.up * fireballSpeed;
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
