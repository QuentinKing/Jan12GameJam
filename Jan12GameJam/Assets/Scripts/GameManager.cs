using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera camera;
    protected static GameManager current;
    private float totalTime = 120f; // 2 min
    public Player player;
    private float score;
    private GameObject[] collectables = GameObject.FindObjectsOfType<Collectable>();

    void Awake()
    {
        GameManager.current = this;
        DontDestroyOnLoad(this);
    }

    public static GameManager GetCurrent()
    {
        return GameManager.current;
    }

    void Update()
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
}
