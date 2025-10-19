using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game States")]
    public GameObject splashScreenState;
    public GameObject mainMenuState;
    public GameObject gameplayState;
    public GameObject gameOverState;
    public GameObject settingsState;
    public GameObject controlsState;
    public GameObject creditsState;

    [Header("Level Data")]
    public LevelData currentLevelData;

    [Header("Prefabs")]
    public GameObject playerPawnPrefab;
    public GameObject playerControllerPrefab;
    public GameObject astronautPrefab;
    public GameObject ufoPrefab;
    public GameObject meteorPrefab;
    public GameObject healthPackPrefab;
    public GameObject projectilePrefab;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip shootSound;
    public AudioClip scoreSound;
    public AudioClip deathSound;

    [Header("Camera Settings")]
    public Transform objectToFollow; //Also used for UFO Controller
    public Vector3 cameraOffset;
    public Vector3 lookOffset;
    public float cameraMoveSpeed;
    public float cameraRotationSpeed;

    [Header("Player Settings")]
    public float playerMoveSpeed;
    public float playerTurnSpeed;
    public float playerFireRate;
    public float playerProjectileSpeed;
    public float playerMaxHealth;
    public int playerStartingLives;

    [Header("UFO Settings")]
    public float ufoMoveSpeed;
    public float ufoTurnSpeed;
    public float ufoFireRate;
    public float ufoProjectileSpeed;
    public float ufoMaxHealth;
    public float ufoSpawnInterval;

    [Header("Projectile Settings")]
    public float projectileLifetime;
    public float projectileDamage;


    [Header("Score Value Settings")]
    public float astronautScore;

    [Header("Score Tracking")]
    public float score = 0f;
    public float topScore = 0f;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        SpawnPlayer();
    }

    private void Update()
    {
        StartGameplay();
    }

    public void SpawnPlayer()
    {
        // Get spawn position and rotation from LevelData
        Vector3 spawnPosition = currentLevelData.playerSpawnPoint.position;
        Quaternion spawnRotation = currentLevelData.playerSpawnPoint.rotation;

        // Instantiate the controller at the spawn point
        GameObject controllerObj = Instantiate(playerControllerPrefab, spawnPosition, spawnRotation);
        PlayerController controller = controllerObj.GetComponent<PlayerController>();
        currentLevelData.players.Add(controller);

        // Instantiate the pawn at the same position and rotation
        GameObject pawnObj = Instantiate(playerPawnPrefab, spawnPosition, spawnRotation);
        Pawn pawn = pawnObj.GetComponent<Pawn>();
        


        if (pawn != null)
        {
            controller.pawn = pawn;
            objectToFollow = pawn.transform;
        }
        else
        {
            Debug.LogWarning("Pawn component missing on playerPawnPrefab.");
        }
    }


    public void SpawnAstronauts()
    {
        foreach (Transform point in currentLevelData.astronautSpawnPoints)
        {
            Instantiate(astronautPrefab, point.position, point.rotation);
        }
    }

    public void SpawnHealthPacks()
    {
        foreach (Transform point in currentLevelData.healPackSpawnPoints)
        {
            Instantiate(healthPackPrefab, point.position, point.rotation);
        }
    }

    public void SpawnEnemies()
    {
        foreach (Transform point in currentLevelData.enemySpawnPoints)
        {
            GameObject enemyToSpawn = GetRandomEnemy(); // UFO or meteor
            GameObject enemyObj = Instantiate(enemyToSpawn, point.position, point.rotation);

            // Only do controller + pawn hookup if it's a UFO
            if (enemyToSpawn == ufoPrefab)
            {
                UFOController controller = enemyObj.AddComponent<UFOController>();
                UFOPawn pawn = enemyObj.GetComponent<UFOPawn>();

                if (pawn != null)
                {
                    controller.pawn = pawn;
                    controller.playerTarget = objectToFollow; // usually the player
                }
                else
                {
                    Debug.LogWarning("UFOPawn component missing on ufoPrefab.");
                }
            }
        }
    }


    public GameObject GetRandomEnemy()
    {
        float roll = Random.Range(0f, 1f);

        if (roll < currentLevelData.ufoChance)
        {
            return ufoPrefab;
        }
        else
        {
            return meteorPrefab;
        }
    }
    public Vector3 GetRandomSpawnPoint()
    {
        return currentLevelData.enemySpawnPoints[Random.Range(0, currentLevelData.enemySpawnPoints.Count)].position;
    }

    // Remove enemy from tracking list
    public void RemoveEnemy(GameObject enemy)
    {
        if (currentLevelData.activeEnemies.Contains(enemy)) currentLevelData.activeEnemies.Remove(enemy);

        // Check for victory condition
        if (currentLevelData.activeEnemies.Count == 0 && currentLevelData.initialEnemiesSpawned >= currentLevelData.enemyCount)
        {
            //ShowGameOver();
        }

        
    }
    public void AddScore(float amount)
    {
        score += amount;

        if (score > topScore)
        {
            topScore = score;
            PlayerPrefs.SetFloat("TopScore", topScore);
            PlayerPrefs.Save();
        }
    }
    public void SpawnHealPickup()
    {
        if (currentLevelData.players.Count == 0 || currentLevelData.players[0].pawn == null)
        {
            return;
        }

        Vector3 playerPos = currentLevelData.players[0].pawn.transform.position;
        Vector3 offset = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);
        Vector3 spawnPos = playerPos + offset;

        GameObject pickup = Instantiate(healthPackPrefab, spawnPos, Quaternion.identity);
        currentLevelData.activeHealPickups.Add(pickup);
    }

    public bool IsPlayerAlive()
    {
        return currentLevelData.players.Count > 0 && currentLevelData.players[0].pawn != null;
    }

    public void StartGameplay()
    {
        if (!IsPlayerAlive())
        {
            return;
        }
        // Meteor spawning
        currentLevelData.enemySpawnTimer += Time.deltaTime;
        if (currentLevelData.enemySpawnTimer >= currentLevelData.enemySpawnInterval && currentLevelData.initialEnemiesSpawned < currentLevelData.enemyCount)
        {
            SpawnEnemies();
            currentLevelData.enemySpawnTimer = 0f;
        }

        // Heal pickup spawning
        currentLevelData.healSpawnTimer += Time.deltaTime;
        if (currentLevelData.healSpawnTimer >= currentLevelData.healSpawnInterval)
        {
            SpawnHealPickup();
            currentLevelData.healSpawnTimer = 0f;
        }
    }
}
