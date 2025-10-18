using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance for global access
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

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip shootSound;
    public AudioClip scoreSound;
    public AudioClip deathSound;

    [Header("Camera Settings")]
    public Transform objectToFollow;
    public Vector3 cameraOffset;
    public Vector3 lookOffset;
    public float cameraMoveSpeed;
    public float cameraRotationSpeed;

    [Header("Player Settings")]
    public float playerMoveForce;
    public float playerTurnSpeed;
    public float playerFireRate;
    public float playerProjectileSpeed;
    public float playerMaxHealth;

    [Header("UFO Settings")]
    public float ufoMoveSpeed;
    public float ufoFireRate;
    public float ufoProjectileSpeed;
    public float ufoMaxHealth;
    public float ufoSpawnInterval;

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

}
