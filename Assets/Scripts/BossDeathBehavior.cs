using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BossDeathBehavior : StateMachineBehaviour
{
    public GameObject princessPrefab;
    public GameObject smallExplosionPrefab;
    public GameObject bigExplosionPrefab;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float timeBetweenSmallExplosions;
    public float timeToEndSmallExplosions;
    public float timeToBigExplosion;
    public float timeAfterBigExplosion;
    public float smallExplosionShakeStrength;
    public float smallExplosionShakeDuration;
    public float bigExplosionShakeStrength;
    public float bigExplosionShakeDuration;

    float startTime;
    float lastSmallExplosionTime;
    bool bigExplosioned;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        startTime = Time.time;
        bigExplosioned = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.time > startTime + timeToBigExplosion && !bigExplosioned)
        {
            bigExplosioned = true;
            Destroy(animator.gameObject.GetComponentInChildren<SpriteRenderer>());
            GameObject bigExplosion = GameObject.Instantiate(bigExplosionPrefab);
            bigExplosion.transform.position = animator.transform.position + new Vector3(0, 0.3f, -2);
            Camera.main.transform.position = new Vector3(0, 0, -10);
            Camera.main.transform.DOShakePosition(bigExplosionShakeDuration, bigExplosionShakeStrength);
            GameObject princess = Instantiate(princessPrefab);
            princess.transform.position = animator.transform.position;
            Destroy(animator.GetComponentInChildren<SpriteRenderer>());
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().DestroyAllProjectiles();
        }

        if (Time.time > lastSmallExplosionTime + timeBetweenSmallExplosions && Time.time < startTime + timeToEndSmallExplosions)
        {
            Vector3 pos = animator.transform.position + new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), -2);
            lastSmallExplosionTime = Time.time;
            GameObject explosion = GameObject.Instantiate(smallExplosionPrefab);
            explosion.transform.position = pos;
            Camera.main.transform.position = new Vector3(0, 0, -10);
            Camera.main.transform.DOShakePosition(smallExplosionShakeDuration, smallExplosionShakeStrength);
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
