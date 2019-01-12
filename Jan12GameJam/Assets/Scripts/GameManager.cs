using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera camera;
    protected static GameManager current;
    private float totalTime = 120f; // 2 min
    public GameObject player;
    private float score;

    void Awake()
    {
        GameManager.current = this;
        DontDestroyOnLoad(this);
    }

    public static GameManager getCurrent()
    {
        return GameManager.current;
    }

    void Update()
    {
        totalTime -= Time.deltaTime;
        if (totalTime < 0 && player.health < 0)
        {
            // UI game over message
        }
    }
}
