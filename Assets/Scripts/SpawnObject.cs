using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject toSpawn;
    public float timeToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn(timeToSpawn));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Spawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject instance = Instantiate(toSpawn);
        instance.transform.position = transform.position;
        Destroy(this.gameObject);
    }
}
