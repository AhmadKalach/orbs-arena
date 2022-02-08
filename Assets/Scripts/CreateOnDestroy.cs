using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOnDestroy : MonoBehaviour
{
    public GameObject createOnDestroy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameObject deathEffect = Instantiate(createOnDestroy);
        deathEffect.transform.position = transform.position;
        deathEffect.transform.rotation = Quaternion.identity;
    }
}
