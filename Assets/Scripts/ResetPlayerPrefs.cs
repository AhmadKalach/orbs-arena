using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{
    public int wave = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("checkpoint", wave);
    }

    private void Update()
    {
        PlayerPrefs.SetInt("checkpoint", wave);
    }
}
