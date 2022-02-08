using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerBehavior : MonoBehaviour
{
    AudioSource audioSource;

    public static bool playing = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
        Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Play()
    {
        if (!playing)
        {
            audioSource.Play();
            playing = true;
        }
    }
}
