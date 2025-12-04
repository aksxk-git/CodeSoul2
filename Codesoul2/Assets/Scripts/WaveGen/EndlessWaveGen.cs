using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class EndlessWaveGen : MonoBehaviour
{
    // Script Reference
    public GameManager gameManager;

    // Text
    [Header("UI")]
    public TMP_Text waveTextUI;
    public TMP_Text countdownTextUI;

    // Spawners
    [Header("Enemy Spawn Locations")]
    public Transform defaultSpawnLocation;
    public GameObject[] spawners;

    // Enemy List
    [Header("Enemies To Spawn")]
    public GameObject[] tier1;
    public GameObject[] tier2;
    public GameObject[] tier3;
    public GameObject[] tier4;
    public GameObject[] tier5;

    // Enemy Information
    public int enemyHpModifier = 0;
    public int enemyPointRewardModifier = 0;

    // Wave Information
    public float countdownUntilNextWave = 30;
    public int currentWave = 0;
    int enemiesSpawned = 0;
    int enemiesToSpawn = 2;
    float spawnInterval = 2;
    public int enemyTier = 1;
    public int unlockTier = 0;
    public bool readyToSpawn = true;
    public bool spawningHasCompleted = false;

    private void Awake()
    {
        // Get Script References
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    private void Start()
    {
        ProceedToNextWave();
    }

    public void Update()
    {
        CheckIfAllEnemiesHaveBeenSpawned();

        if (isEnemyInScene())
        {
            countdownTextUI.text = string.Empty;
            countdownUntilNextWave = 30;
        }
        else if (!isEnemyInScene() && spawningHasCompleted)
        {
            countdownTextUI.text = "TIME UNTIL NEXT WAVE: " + countdownUntilNextWave.ToString("F0");
            countdownUntilNextWave -= Time.deltaTime;
        }

        if (countdownUntilNextWave <= 0 && readyToSpawn && !isEnemyInScene())
        {
            ProceedToNextWave();
        }
    }

    private void UpdateEnemyHP(GameObject Enemy)
    {
        //Enemy.GetComponent<Enemy>().hp += enemyHpModifier;
    }

    private void CheckIfAllEnemiesHaveBeenSpawned()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemiesSpawned >= enemiesToSpawn)
        {
            spawningHasCompleted = true;
        }
        else
        {
            spawningHasCompleted = false;
        }
    }

    public void EndCountdown()
    {
        countdownUntilNextWave = 0;
    }

    private void ProceedToNextWave()
    {
        // Update Current Wave
        currentWave++;
        enemiesSpawned = 0;
        waveTextUI.text = currentWave.ToString();

        gameManager.wave = currentWave;

        // Update Modifiers
        enemyHpModifier += 3;
        //enemyPointRewardModifier += 25;
        enemiesToSpawn += 3;
        unlockTier++;

        // every 5 waves unlock an enemy tier, enemy tiers introduce new enemies
        if (unlockTier == 5)
        {
            unlockTier = 0;
            enemyTier++;
        }

        // Spawn Enemies
        StartCoroutine(SpawnInterval());
    }

    private Vector2 GiveRandomPosition()
    {
        //Vector2 randomSpawnPosition = spawners[Random.Range(0, spawners.Length)].transform.position;
        //return randomSpawnPosition;

        GameObject randomSpawner = spawners[Random.Range(0, spawners.Length)];
        if (randomSpawner.activeSelf)
        {
            return randomSpawner.transform.position;
        }
        else
        {
            return defaultSpawnLocation.position;
        }

    }

    private void SpawnEnemies(Vector2 position)
    {
        if (enemyTier == 1)
        {
            GameObject randomEnemy = tier1[Random.Range(0, tier1.Length)];
            Instantiate(randomEnemy, position, Quaternion.identity);
        }
        else if (enemyTier == 2)
        {
            GameObject randomEnemy = tier2[Random.Range(0, tier2.Length)];
            Instantiate(randomEnemy, position, Quaternion.identity);
        }
        else if (enemyTier == 3)
        {
            GameObject randomEnemy = tier3[Random.Range(0, tier3.Length)];
            Instantiate(randomEnemy, position, Quaternion.identity);
        }
        else if (enemyTier == 4)
        {
            GameObject randomEnemy = tier4[Random.Range(0, tier4.Length)];
            Instantiate(randomEnemy, position, Quaternion.identity);
        }
        else
        {
            GameObject randomEnemy = tier5[Random.Range(0, tier5.Length)];
            Instantiate(randomEnemy, position, Quaternion.identity);
        }
    }

    private bool isEnemyInScene()
    {
        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator SpawnInterval()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemies(GiveRandomPosition());
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnInterval);
        }

        readyToSpawn = true;
    }
}