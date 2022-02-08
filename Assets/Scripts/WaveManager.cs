using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [Header("Configuration")]
    public int currCheckPoint;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public bool destroyProjectilesOnWaveEnd;
    public float projectileDestroyTime;
    public TextMeshProUGUI waveUI;
    public float waveTextStartY;
    public float waveTextTargetY;
    public float timeBeforewaveTextAnimationTime;
    public float waveTextAnimationTime;
    public float waveTextWaitTime;

    [Header("Enemies")]
    public char batChar;
    public GameObject batPrefab;
    public char radishChar;
    public GameObject radishPrefab;
    public char rabbitChar;
    public GameObject rabbitPrefab;
    public char healthChar;
    public GameObject healthPrefab;
    public char bossChar;
    public GameObject bossPrefab;
    public float bossWaitTime;
    public Transform bossPosition;
    public char checkpointChar;
    public char seperator;

    [Header("Waves")]
    public List<string> waves;

    float nextWaveMinEndTime;
    bool lastWaveEnded;
    public int waveIndex;
    float lastWaveEndTime;
    List<GameObject> currEnemies;
    List<GameObject> currProjectiles;
    bool freezeLastWaveEnded;
    Player player;


    // Start is called before the first frame update
    void Start()
    {
        waveIndex = PlayerPrefs.GetInt("checkpoint", 0);
        currEnemies = new List<GameObject>();
        currProjectiles = new List<GameObject>();
        lastWaveEndTime = Time.time;
        lastWaveEnded = true;
        waveUI.rectTransform.anchoredPosition = new Vector2(waveUI.rectTransform.anchoredPosition.x, waveTextStartY);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void AnimateWaveUI(string toDisplay)
    {
        waveUI.text = toDisplay;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(waveUI.rectTransform.DOAnchorPosY(waveTextTargetY, waveTextAnimationTime).SetEase(Ease.OutQuint));
        sequence.Append(waveUI.rectTransform.DOAnchorPosY(waveTextTargetY, waveTextWaitTime).SetEase(Ease.OutQuint));
        sequence.Append(waveUI.rectTransform.DOAnchorPosY(waveTextStartY, waveTextAnimationTime).SetEase(Ease.InQuint));
        sequence.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //todo optimize later
        if (player != null && GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && waveIndex < waves.Count && !freezeLastWaveEnded && Time.time > nextWaveMinEndTime)
        {
            if (!lastWaveEnded)
            {
                lastWaveEndTime = Time.time;
                lastWaveEnded = true;
            }
            if (Time.time > lastWaveEndTime)
            {
                lastWaveEnded = false;
                string currWave = waves[waveIndex];
                ProcessWave(currWave);
                freezeLastWaveEnded = true;
                StartCoroutine(ChangeFreezeLastWaveEnd());
                if (destroyProjectilesOnWaveEnd)
                {
                    StartCoroutine(DestroyProjectiles());
                }
                waveIndex++;
            }
        }
    }

    IEnumerator WaitThenAnimateText(float waitTime, string text)
    {
        yield return new WaitForSeconds(waitTime);
        AnimateWaveUI(text);
    }

    IEnumerator DestroyProjectiles()
    {
        yield return new WaitForSeconds(projectileDestroyTime);
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject projectile in projectiles)
        {
            Destroy(projectile.gameObject);
        }
    }

    IEnumerator ChangeFreezeLastWaveEnd()
    {
        yield return new WaitForSeconds(1);
        freezeLastWaveEnded = false;
    }

    void ProcessWave(string wave)
    {
        string[] seperatedString = wave.Split(seperator);
        float waitTime = 0;
        foreach (string toParse in seperatedString)
        {
            char currChar = toParse.ToCharArray()[0];
            if (currChar.Equals(batChar))
            {
                StartCoroutine(Spawn(waitTime, batPrefab));
            }
            else if (currChar.Equals(radishChar))
            {
                StartCoroutine(Spawn(waitTime, radishPrefab));
            }
            else if (currChar.Equals(rabbitChar))
            {
                StartCoroutine(Spawn(waitTime, rabbitPrefab));
            }
            else if (currChar.Equals(checkpointChar))
            {
                PlayerPrefs.SetInt("checkpoint", waveIndex + 1);
                StartCoroutine(WaitThenAnimateText(timeBeforewaveTextAnimationTime, "Checkpoint"));
            }
            else if (currChar.Equals(healthChar))
            {
                StartCoroutine(SpawnAtPosition(Vector2.zero, healthPrefab, waitTime));
            }
            else if (currChar.Equals(bossChar))
            {
                StartCoroutine(SpawnAtPosition(bossPosition.transform.position, bossPrefab, bossWaitTime));
                //StartCoroutine(WaitThenAnimateText(timeBeforewaveTextAnimationTime, "Fire Lord"));
            }
            else
            {
                waitTime += float.Parse(toParse);
            }
        }
        nextWaveMinEndTime = Time.time + waitTime;
    }

    IEnumerator Spawn(float delay, GameObject toSpawn)
    {
        yield return new WaitForSeconds(delay);
        Vector2 randomPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        StartCoroutine(SpawnAtPosition(randomPos, toSpawn, 0));
    }

    IEnumerator SpawnAtPosition(Vector2 position, GameObject toSpawn, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject enemy = Instantiate(toSpawn);
        enemy.transform.position = position;
        currEnemies.Add(enemy);
    }
}
