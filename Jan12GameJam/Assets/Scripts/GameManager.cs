using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera camera;
    public UI_Manager uiManager;
    protected static GameManager current;
    public Collectable collectableClass;
    public EnemyMovement enemyClass;
    private float totalTime = 120f; // 2 min
    public Player player;
    private float score;

    public int numCollectables = 10;
    public int numEnemies = 3;
    public float minX = -8.0f;
    public float maxX = 8.0f;
    public float minZ = -8.0f;
    public float maxZ = 8.0f;
    public float collectableY = 1.0f;

    protected void Awake()
    {
        GameManager.current = this;
        DontDestroyOnLoad(this);
    }

    protected void Start() {
        GenerateCollectables();
        GenerateEnemies();
    }

    public static GameManager GetCurrent()
    {
        return GameManager.current;
    }

    protected void Update()
    {
        totalTime -= Time.deltaTime;
        if (totalTime < 0)
        {
            if (numCollectables > 0 || player.GetLives() < 0)
            {
                // UI game over message
                uiManager.OnGameOver(false);
            }
        }
        else
        {
            if (numCollectables <= 0)
            {
                // UI win message
                uiManager.OnGameOver(true);
            }
        }
        uiManager.UpdateText(player.GetLives(), (int)score, player.GetStamina() / player.GetMaxStamina(), totalTime);
    }

    public float randRange(float a, float b) {
        return a + (b - a) * Random.value;
    }

    public void GenerateCollectables() {
        for (int i = 0; i < numCollectables; i++) {
            Instantiate(
                collectableClass,
                new Vector3(randRange(minX, maxX), collectableY, randRange(minZ, maxZ)),
                Quaternion.identity
            );
        }
    }

    public void GenerateEnemies() {
        for (int i = 0; i < numEnemies; i++) {
            var enemy = Instantiate(
                enemyClass,
                new Vector3(randRange(minX, maxX), collectableY, randRange(minZ, maxZ)),
                Quaternion.identity
            );
            enemy.player = player.transform;
        }
    }
}
