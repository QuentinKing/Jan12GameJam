using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera camera;
    protected static GameManager current;
    public Collectable collectableClass;
    private float totalTime = 120f; // 2 min
    public Player player;
    private float score;
    private GameObject[] collectables = GameObject.FindObjectsOfType<Collectable>();

    public int numCollectables = 10;
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
            if (collectables.Length > 0 || player.GetLives() < 0)
            {
                // UI game over message
            }
        }
        else
        {
            if (collectables.Length <= 0)
            {
                // UI win message
            }
        }
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
}
