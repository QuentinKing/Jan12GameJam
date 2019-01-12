using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody rb;
    private Vector3 up = Vector3(1.0f, 0.0f, 1.0f);
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Movement() {
        if (Input.GetKeyDown(KeyCode.W)) {
            
        }
    }

    void FixedUpdate() {
        // apply force
        Movement();
        // friction
    }
}
