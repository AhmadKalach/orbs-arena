using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score;
    public GameObject winText;
    public GameObject restartButton;
    public GameObject continueButton;
    public GameObject winCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            restartButton.SetActive(true);
            continueButton.SetActive(true);
        }
    }

    private IEnumerator RestartGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("SampleScene");
    }

    void WinScreenLegacy()
    {
        restartButton.SetActive(true);
        winText.SetActive(true);
    }

    public void WinScreen()
    {
        winCanvas.SetActive(true);
    }

    public void DestroyAllProjectiles()
    {
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach(GameObject projectile in projectiles)
        {
            Destroy(projectile.gameObject);
        }
    }
}
