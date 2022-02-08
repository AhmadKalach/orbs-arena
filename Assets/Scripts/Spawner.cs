using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float minX, maxX, minY, maxY;
    public GameObject toSpawn;
    public float timeBetweenSpawns;

    float lastSpawnTime;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = Time.time;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Time.time > lastSpawnTime + timeBetweenSpawns)
            {
                lastSpawnTime = Time.time;
                float x = Random.Range(minX, maxX);
                float y = Random.Range(minY, maxY);
                GameObject instance = Instantiate(toSpawn);
                instance.transform.position = new Vector2(x, y);
            }
        }
    }
}
