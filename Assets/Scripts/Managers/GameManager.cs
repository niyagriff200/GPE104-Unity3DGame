using System.Collections.Generic;
using UnityEngine;
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

    [Header("GameplayUI GUI")]
    public GameplayUI gameplayUI;

    [Header("Prefabs")]
    public GameObject playerPawnPrefab;
    public GameObject playerControllerPrefab;
    public GameObject astronautPrefab;
    public GameObject ufoPrefab;
    public GameObject meteorPrefab;
    public GameObject healthPackPrefab;
    public GameObject playerProjectilePrefab;
    public GameObject ufoProjectilePrefab;

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
    public float playerMaxHealth;
    public int startingLives;

    [Header("UFO Settings")]
    public float ufoMoveSpeed;
    public float ufoTurnSpeed;
    public float ufoFireRate;
    public float ufoMaxHealth;
    public float ufoStoppingDistance;

    [Header("Projectile Settings")]
    public GameObject defaultProjectile;
    public float projectileLifetime;
    public float projectileSpeed;
    public float projectileDamage;


    [Header("Score Value Settings")]
    public float astronautScore;
    public float ufoScore;

    [Header("Score Tracking")]
    public float score = 0f;
    public float topScore = 0f;

    // Enforce singleton pattern
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

        ShowSplashScreen();
    }

    private void Start()
    {

        topScore = PlayerPrefs.GetFloat("TopScore", 0f);
        //PlayBackgroundMusic musicManager = Object.FindFirstObjectByType<PlayBackgroundMusic>();
        //musicManager.PlayMenuMusic();
    }

    private void Update()
    {
        if (gameplayState.activeInHierarchy)
        {
            StartGameplay();
        }

        if (currentLevelData.players.Count > 0 && currentLevelData.players[0].pawn == null)
        {
            ShowGameOver();
        }
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
        PlayerPawn pawn = pawnObj.GetComponent<PlayerPawn>();
        


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


   /* public void SpawnAstronauts()
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
    }*/

    public void SpawnEnemy()
    {
        if (currentLevelData == null)
        {
            return;
        }

        // Pick one random spawn point
        if (currentLevelData.enemySpawnPoints.Count == 0)
        {
            return;
        }

        Transform randomPoint = currentLevelData.enemySpawnPoints[Random.Range(0, currentLevelData.enemySpawnPoints.Count)];

        GameObject enemyToSpawn = GetRandomEnemy();
        GameObject enemyObj = Instantiate(enemyToSpawn, randomPoint.position, randomPoint.rotation);

        // Track the enemy
        currentLevelData.activeEnemies.Add(enemyObj);
        currentLevelData.initialEnemiesSpawned++;

        // Hook up controller + pawn if it's a UFO
        if (enemyToSpawn == ufoPrefab)
        {
            UFOController controller = enemyObj.AddComponent<UFOController>();
            UFOPawn pawn = enemyObj.GetComponent<UFOPawn>();

            if (pawn != null)
            {
                controller.pawn = pawn;
                controller.playerTarget = objectToFollow;
            }
            else
            {
                Debug.LogWarning("UFOPawn component missing on ufoPrefab.");
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
        if (currentLevelData.healPackSpawnPoints.Count == 0)
        {
            return;
        }
        Transform spawnPoint = currentLevelData.healPackSpawnPoints[Random.Range(0, currentLevelData.healPackSpawnPoints.Count)];

        GameObject pickup = Instantiate(healthPackPrefab, spawnPoint.position, spawnPoint.rotation);
        currentLevelData.activeHealPickups.Add(pickup);
    }

    public void SpawnAstronaut()
    {
        // Do nothing if there are no spawn points assigned
        if (currentLevelData.astronautSpawnPoints.Count == 0)
        {
            return;
        }

        // Pick a random spawn point from the list
        Transform spawnPoint = currentLevelData.astronautSpawnPoints[Random.Range(0, currentLevelData.astronautSpawnPoints.Count)];

        // Create the astronaut and track it
        GameObject astronautObj = Instantiate(astronautPrefab, spawnPoint.position, spawnPoint.rotation);
        currentLevelData.activeAstronauts.Add(astronautObj);
        currentLevelData.initialAstronautsSpawned++;
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
            SpawnEnemy();
            currentLevelData.enemySpawnTimer = 0f;
        }

        // Heal pickup spawning
        currentLevelData.healSpawnTimer += Time.deltaTime;
        if (currentLevelData.healSpawnTimer >= currentLevelData.healSpawnInterval)
        {
            SpawnHealPickup();
            currentLevelData.healSpawnTimer = 0f;
        }

        currentLevelData.astronautSpawnTimer += Time.deltaTime;
        if (currentLevelData.astronautSpawnTimer >= currentLevelData.astronautSpawnInterval && currentLevelData.initialAstronautsSpawned < currentLevelData.astronautCount)
        {
            SpawnAstronaut();
            currentLevelData.astronautSpawnTimer = 0f;
        }
    }
    public void ShowSplashScreen()
    {
        splashScreenState.SetActive(true);
        mainMenuState.SetActive(false);
        gameplayState.SetActive(false);
        gameOverState.SetActive(false);
        creditsState.SetActive(false);
        controlsState.SetActive(false);
        settingsState.SetActive(false);
    }

    public void ShowMainMenu()
    {
        splashScreenState.SetActive(false);
        mainMenuState.SetActive(true);
        gameplayState.SetActive(false);
        gameOverState.SetActive(false);
        creditsState.SetActive(false);
        controlsState.SetActive(false);
        settingsState.SetActive(false);

    }

    public void ShowGameplay()
    {
        splashScreenState.SetActive(false);
        mainMenuState.SetActive(false);
        gameplayState.SetActive(true);
        gameOverState.SetActive(false);
        creditsState.SetActive(false);
        controlsState.SetActive(false);
        settingsState.SetActive(false);

        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            AudioListener listener = mainCam.GetComponent<AudioListener>();
            if (listener != null)
            {
                listener.enabled = false;
            }
        }


        gameplayUI.InitializeLives(startingLives);
        gameplayUI.UpdateLives(startingLives);

        currentLevelData.players = new List<PlayerController>();
        currentLevelData.enemySpawnTimer = 0f;
        currentLevelData.healSpawnTimer = 0f;
        score = 0f;
        currentLevelData.initialEnemiesSpawned = 0;

        AudioListener playerListener = playerPawnPrefab.GetComponent<AudioListener>();
        if (playerListener != null)
        {
            playerListener.enabled = true;
        }

        /*PlayBackgroundMusic musicManager = Object.FindFirstObjectByType<PlayBackgroundMusic>();
        musicManager.PlayGameplayMusic();*/



        currentLevelData.activeEnemies.Clear();
        SpawnPlayer();


    }

    public void ShowGameOver()
    {
        splashScreenState.SetActive(false);
        mainMenuState.SetActive(false);
        gameplayState.SetActive(false);
        gameOverState.SetActive(true);
        creditsState.SetActive(false);
        controlsState.SetActive(false);
        settingsState.SetActive(false);

        // Destroy enemies
        foreach (GameObject enemy in currentLevelData.activeEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        currentLevelData.activeEnemies.Clear();

        // Destroy players (controller + pawn)
        foreach (PlayerController controller in currentLevelData.players)
        {
            if (controller != null)
            {
                // Destroy the pawn GameObject if present
                if (controller.pawn != null)
                {
                    Destroy(controller.pawn.gameObject);
                }

                // Destroy the controller object
                Destroy(controller.gameObject);
            }
        }
        currentLevelData.players.Clear();

        // Destroy heal pickups that were tracked
        foreach (GameObject pickup in currentLevelData.activeHealPickups)
        {
            if (pickup != null)
            {
                Destroy(pickup);
            }
        }
        currentLevelData.activeHealPickups.Clear();

        GameOverUI gameOverUI = gameOverState.GetComponentInChildren<GameOverUI>();
        if (gameOverUI != null)
        {
            gameOverUI.ShowResults();
        }

        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            AudioListener listener = mainCam.GetComponent<AudioListener>();
            if (listener != null)
            {
                listener.enabled = true;
            }
        }

        /*PlayBackgroundMusic musicManager = Object.FindFirstObjectByType<PlayBackgroundMusic>();
        musicManager.PlayMenuMusic();*/
    }


    public void ShowCreditsScreen()
    {
        splashScreenState.SetActive(false);
        mainMenuState.SetActive(false);
        gameplayState.SetActive(false);
        gameOverState.SetActive(false);
        creditsState.SetActive(true);
        controlsState.SetActive(false);
        settingsState.SetActive(false);
    }

    public void ShowControlsScreen()
    {
        splashScreenState.SetActive(false);
        mainMenuState.SetActive(false);
        gameplayState.SetActive(false);
        gameOverState.SetActive(false);
        creditsState.SetActive(false);
        controlsState.SetActive(true);
        settingsState.SetActive(false);
    }

    public void ShowSettingsScreen()
    {
        splashScreenState.SetActive(false);
        mainMenuState.SetActive(false);
        gameplayState.SetActive(false);
        gameOverState.SetActive(false);
        creditsState.SetActive(false);
        controlsState.SetActive(false);
        settingsState.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
