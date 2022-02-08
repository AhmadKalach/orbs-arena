using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySfxOnCreate : MonoBehaviour
{
    public AudioClip sfx;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
