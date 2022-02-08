using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyBehavior : MonoBehaviour
{
    public float moveSpeed;
    public float timeBetweenBombs;
    public GameObject bomb;
    public GameObject sprite;

    Rigidbody2D rb;
    float lastBombTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 runDir = Random.insideUnitCircle.normalized;
        rb.velocity = runDir * moveSpeed;
        lastBombTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x > 0)
        {
            sprite.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (rb.velocity.x < 0)
        {
            sprite.transform.rotation = Quaternion.identity;
        }

        if (Time.time > lastBombTime + timeBetweenBombs)
        {
            lastBombTime = Time.time;
            GameObject currBomb = GameObject.Instantiate(bomb);
            currBomb.transform.position = transform.position;
        }
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
