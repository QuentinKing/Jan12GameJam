using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Camera camera;

    protected static GameManager current;

    void Awake() {
        GameManager.current = this;
        DontDestroyOnLoad(this);
    }

    public static GameManager getCurrent() {
        return GameManager.current;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
