using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeartBehavior : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float timeToDisappear;
    public float yDiff;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.DOMoveY(transform.position.y + yDiff, timeToDisappear);
        spriteRenderer.DOFade(0, timeToDisappear).OnComplete(() => Destroy(this.gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
