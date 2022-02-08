using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayOnDestroy : MonoBehaviour
{
    public AudioClip destroySound;
    public float volume;
    public float shakeStrength;
    public float shakeDuration;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position, volume);
        Camera.main.transform.DOShakePosition(shakeDuration, shakeStrength);
    }
}
