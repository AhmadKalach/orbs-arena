using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewHealthUI : MonoBehaviour
{
    public Color fullColor;
    public Color emptyColor;
    public Image hp1;
    public Image hp2;
    public Image hp3;
    public Image hp4;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (player.currHealth == 4)
            {
                hp1.color = fullColor;
                hp2.color = fullColor;
                hp3.color = fullColor;
                hp4.color = fullColor;
            }
            if (player.currHealth == 3)
            {
                hp1.color = fullColor;
                hp2.color = fullColor;
                hp3.color = fullColor;
                hp4.color = emptyColor;
            }
            if (player.currHealth == 2)
            {
                hp1.color = fullColor;
                hp2.color = fullColor;
                hp3.color = emptyColor;
                hp4.color = emptyColor;
            }
            if (player.currHealth == 1)
            {
                hp1.color = fullColor;
                hp2.color = emptyColor;
                hp3.color = emptyColor;
                hp4.color = emptyColor;
            }
        }
        else
        {
            hp1.color = emptyColor;
            hp2.color = emptyColor;
            hp3.color = emptyColor;
            hp4.color = emptyColor;
        }
    }
}
