using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<Player>().GetDamaged(1);
            Destroy(this.gameObject);
        }
    }
}
