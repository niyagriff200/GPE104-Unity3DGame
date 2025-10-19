using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [Header("Player Movement Limit")]
    public float playerMaxY; // Designer-defined ceiling

    [Header("Player Settings")]
    public Transform playerSpawnPoint;
    public List<PlayerController> players;

    [Header("Level Dimensions")]
    public Vector3 levelSize = new Vector3(100.0f, 100.0f, 100.0f);

    [Header("Environment Settings")]
    public AudioClip levelMusic;
    public Material groundMaterial;

    [Header("Spawn Points")]
    public List<Transform> enemySpawnPoints;
    public List<Transform> healPackSpawnPoints;
    public List<Transform> astronautSpawnPoints;

    [Header("Enemy Spawn Settings")]
    public List<GameObject> activeEnemies = new List<GameObject>();
    public int enemyCount;
    [Range(0f, 1f)] public float ufoChance;
    public float enemySpawnInterval;
    [HideInInspector] public float enemySpawnTimer;
    [HideInInspector] public int initialEnemiesSpawned = 0;

    [Header("Heal Pickup Settings")]
    public float healAmount;
    public float healSpawnInterval;
    [HideInInspector] public float healSpawnTimer;
    public List<GameObject> activeHealPickups = new List<GameObject>();

    [Header("Astronaut Spawn Settings")]
    public List<GameObject> activeAstronauts = new List<GameObject>();
    public int astronautCount;
    public float astronautSpawnInterval;
    [HideInInspector] public float astronautSpawnTimer;
    [HideInInspector] public int initialAstronautsSpawned;


    // This component only stores level-specific data so the GameManager or LevelLoader
    // can use it later. It does not perform any logic itself.
}
