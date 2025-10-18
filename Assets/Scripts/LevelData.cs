using UnityEngine;

public class LevelData : MonoBehaviour
{
    [Header("Player Movement Limit")]
    public float playerMaxY; // Designer-defined ceiling

    [Header("Player Settings")]
    public Transform playerSpawnPoint;

    [Header("Level Dimensions")]
    public Vector3 levelSize = new Vector3(100.0f, 100.0f, 100.0f);

    [Header("Environment Settings")]
    public AudioClip levelMusic;
    public Material groundMaterial;

    [Header("Spawn Points")]
    public Transform[] enemySpawnPoints;
    public Transform[] powerupSpawnPoints;

    [Header("Camera Settings")]
    public Transform cameraFocusPoint;

    // This component only stores level-specific data so the GameManager or LevelLoader
    // can use it later. It does not perform any logic itself.
}
