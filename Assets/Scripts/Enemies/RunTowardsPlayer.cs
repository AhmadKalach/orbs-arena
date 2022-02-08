using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTowardsPlayer : MonoBehaviour
{
    public float speed;

    GameObject player;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
            rb.velocity = direction * speed;
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
            rb.velocity = Vector2.zero;
        }
    }
}
