using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOnAwake : MonoBehaviour
{
    public GameObject toCreate;
    public bool forTesting;
    public float timeToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        GameObject instance = Instantiate(toCreate);
        instance.transform.position = transform.position;
        if (timeToDestroy > 0)
        {
            StartCoroutine(DestroyGameObject(instance, timeToDestroy));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (forTesting)
        {
            forTesting = false;
            GameObject i = Instantiate(toCreate);
            i.transform.position = transform.position;
        }
    }

    IEnumerator DestroyGameObject(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
