using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadishBehavior : MonoBehaviour
{
    public float moveSpeed;
    public float distanceToEndAttack;
    public ParticleSystem particleSystem;

    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    Animator animator;
    float lastAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        lastAttackTime = Time.time;
    }

    public void enableCollision()
    {
        boxCollider.enabled = true;
    }

    public void disableCollision()
    {
        boxCollider.enabled = true;
    }

    public void stopMovement()
    {
        rb.velocity = Vector2.zero;
    }

    public void startParticles()
    {
        particleSystem.Play();
    }

    public void stopParticles()
    {
        particleSystem.Stop();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<Player>().GetDamaged(1);
            Destroy(this.gameObject);
        }
    }
}
