using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public int maxHealth;
    public AudioClip damagedSound;
    public AudioClip healSound;
    public SpriteRenderer sprite;
    public float invincibilityTime;
    public Color healColor;
    public Color damagedColor;
    public float damagedShakeStrength;
    public float damagedShakeDuration;

    [HideInInspector]
    public int currHealth;
    bool invincible;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamaged(int amout)
    {
        if (!invincible)
        {
            Camera.main.transform.DOShakePosition(damagedShakeStrength, damagedShakeStrength);
            invincible = true;
            sprite.color = damagedColor;
            AudioSource.PlayClipAtPoint(damagedSound, Camera.main.transform.position);
            currHealth--;
            StartCoroutine(MakeInvincible(invincibilityTime));
            if (currHealth < 1)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void Heal(int amout)
    {
        currHealth += amout;
        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
        AudioSource.PlayClipAtPoint(healSound, Camera.main.transform.position);
        StartCoroutine(HealAnimation(invincibilityTime));
    }

    IEnumerator HealAnimation(float invincibilityTime)
    {
        sprite.color = healColor;
        yield return new WaitForSeconds(invincibilityTime);
        sprite.color = Color.white;
    }

    IEnumerator MakeInvincible(float invincibilityTime)
    {
        yield return new WaitForSeconds(invincibilityTime);
        sprite.color = Color.white;
        invincible = false;
    }
}
