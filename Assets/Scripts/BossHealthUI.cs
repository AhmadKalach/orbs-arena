using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthUI : MonoBehaviour
{
    public GameObject bossUI;
    public List<GameObject> bossHealth;

    BossBehavior bossBehavior;
    float lastTimeout;
    float timeoutTime;

    // Start is called before the first frame update
    void Start()
    {
        timeoutTime = 1f;
        lastTimeout = Time.time;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Boss");
        if (objs.Length > 0)
        {
            bossBehavior = objs[0].GetComponent<BossBehavior>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bossBehavior != null)
        {
            bossUI.SetActive(true);
            if (bossBehavior.currHealth > -1)
            {
                for (int i = 0; i < bossBehavior.currHealth; i++)
                {
                    bossHealth[i].SetActive(true);
                }

                for (int i = bossBehavior.currHealth; i < bossBehavior.maxHealth; i++)
                {
                    bossHealth[i].SetActive(false);
                }
            }
        }
        else
        {
            bossUI.SetActive(false);
            if (Time.time > lastTimeout + timeoutTime)
            {
                lastTimeout = Time.time;

                GameObject[] objs = GameObject.FindGameObjectsWithTag("Boss");
                if (objs.Length > 0)
                {
                    bossBehavior = objs[0].GetComponent<BossBehavior>();
                }
            }
        }
    }
}
