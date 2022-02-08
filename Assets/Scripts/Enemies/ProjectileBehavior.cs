using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProjectileBehavior : MonoBehaviour
{
    public AudioClip wallHitSound;
    public GameObject whiteFlash;
    public GameObject createOnDestroy;
    public float volume;
    public float timeToDisableFlash;
    public float wallHitShakeStrength;
    public float wallHitShakeDuration;
    public float destroyShakeStrength;
    public float destroyShakeDuration;
    public bool canHitPlayer;

    GameManager gameManager;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        whiteFlash.SetActive(true);
        StartCoroutine(WaitThenDisableFlash(timeToDisableFlash));
        canHitPlayer = false;
        StartCoroutine(EnablePlayerHit());
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator EnablePlayerHit()
    {
        yield return new WaitForSeconds(timeToDisableFlash * 2.5f);
        canHitPlayer = true;
    }

    // suspend execution for waitTime seconds
    IEnumerator WaitThenDisableFlash(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        whiteFlash.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (canHitPlayer)
            {
                collision.gameObject.GetComponent<Player>().GetDamaged(1);
                Destroy(this.gameObject);
            }
        }
        else if (collision.gameObject.tag.Equals("Enemy"))
        {
            Destroy(collision.gameObject);
            DestroyFunction();
            gameManager.score += 2;
        }
        else if (collision.gameObject.tag.Equals("Projectile"))
        {
            DestroyFunction();
        }
        else if (collision.gameObject.tag.Equals("Wall"))
        {
            AudioSource.PlayClipAtPoint(wallHitSound, Camera.main.transform.position, volume);
            Camera.main.transform.position = new Vector3(0, 0, -10);
            Camera.main.transform.DOShakePosition(wallHitShakeDuration, wallHitShakeStrength);
        }
        else if (collision.gameObject.tag.Equals("Boss"))
        {
            collision.gameObject.GetComponent<BossBehavior>().GetDamaged();
            DestroyFunction();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Boss"))
        {
            collision.gameObject.GetComponent<BossBehavior>().GetDamaged();
            DestroyFunction();
        }
    }

    void DestroyFunction()
    {
        rb.velocity = Vector2.zero;
        whiteFlash.SetActive(true);
        StartCoroutine(DestroyAfterTime(timeToDisableFlash));
        Camera.main.transform.position = new Vector3(0, 0, -10);
        Camera.main.transform.DOShakePosition(destroyShakeDuration, destroyShakeStrength);
        gameManager.score += 1;
    }

    IEnumerator DestroyAfterTime(float seconds)
    {
        yield return new WaitForSeconds(timeToDisableFlash);
        GameObject deathEffect = Instantiate(createOnDestroy);
        deathEffect.transform.position = transform.position;
        deathEffect.transform.rotation = Quaternion.identity;
        Destroy(this.gameObject);
    }
}
