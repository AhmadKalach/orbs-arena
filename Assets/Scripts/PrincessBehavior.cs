using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessBehavior : MonoBehaviour
{
    public float speed;
    public float distanceToStop;
    public float timeBetweenHearts;
    public GameObject heartPrefab;

    GameObject player;
    Rigidbody2D rb;
    Animator animator;
    float lastHeartTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.transform.position) > distanceToStop)
            {
                Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
                rb.velocity = direction * speed;
                animator.SetTrigger("Run");
            }
            else
            {
                rb.velocity = Vector2.zero;
                animator.SetTrigger("Idle");
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().WinScreen();
            }
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
            rb.velocity = Vector2.zero;
        }
        if (Time.time > lastHeartTime + timeBetweenHearts)
        {
            lastHeartTime = Time.time;
            GameObject heart = Instantiate(heartPrefab);
            heart.transform.position = transform.position;
        }
    }

}
