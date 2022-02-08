using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Color damagedColor;
    public float colorChangeTime;
    public GameObject handPos;
    public int maxHealth;
    public int currHealth;

    bool died;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        GameObject.FindGameObjectWithTag("BossUI").SetActive(true);
        died = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currHealth < 1 && !died)
        {
            GetComponent<Animator>().SetTrigger("Die");
            died = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.tag.Equals("Player"))
        {
            gameObject.GetComponent<Player>().GetDamaged(1);
        }
    }

    public void GetDamaged()
    {
        currHealth--;
        StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor()
    {
        sprite.color = damagedColor;
        yield return new WaitForSeconds(colorChangeTime);
        sprite.color = Color.white;
    }
}
