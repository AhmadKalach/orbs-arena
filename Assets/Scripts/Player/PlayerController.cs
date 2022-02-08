using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Transform attackRotation;
    public Transform attackPosition;
    public float attackRadius;
    public float attackCooldown;
    public float projectileFreezeTime;
    public float projectileSpeed;
    public float hitShakeStrength;
    public float hitShakeDuration;

    float lastAttackTime;
    Rigidbody2D rb;
    Animator animator;
    Transform sprite;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = transform.GetChild(0);
        lastAttackTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetMouseButtonDown(0) && Time.time > lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            animator.SetTrigger("Attack");
        }
    }

    public void Attack()
    {
        Collider2D[] objectsHit = Physics2D.OverlapCircleAll(attackPosition.position, attackRadius);

        foreach (Collider2D collider in objectsHit)
        {
            if (collider.gameObject.tag.Equals("Enemy") || collider.gameObject.tag.Equals("Projectile"))
            {
                Destroy(collider.gameObject);
                GameObject instance = GameObject.Instantiate(collider.gameObject.GetComponent<Launchable>().projectile);
                instance.transform.position = collider.gameObject.transform.position;
                instance.transform.rotation = Quaternion.identity;
                StartCoroutine(Push(instance, attackRotation.transform.rotation));
                Camera.main.transform.DOShakePosition(hitShakeDuration, hitShakeStrength);
            }
            else if (collider.gameObject.tag.Equals("Item"))
            {
                Destroy(collider.gameObject);
                Camera.main.transform.DOShakePosition(hitShakeDuration, hitShakeStrength);
            }
        }
    }

    IEnumerator Push(GameObject instance, Quaternion rotation)
    {

        yield return new WaitForSeconds(projectileFreezeTime);
        instance.transform.rotation = rotation;
        instance.GetComponent<Rigidbody2D>().velocity = instance.transform.up * projectileSpeed;
        instance.transform.rotation = Quaternion.identity;
    }

    void Movement()
    {
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (dir.magnitude > 1)
        {
            dir = dir.normalized;
        }

        if (dir.x > 0)
        {
            sprite.transform.rotation = Quaternion.identity;
        }
        else if (dir.x < 0)
        {
            sprite.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        rb.velocity = dir * speed;

        if (dir.magnitude > 0.1f)
        {
        }

        if (dir.magnitude > 0.6)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRadius);
    }

}
