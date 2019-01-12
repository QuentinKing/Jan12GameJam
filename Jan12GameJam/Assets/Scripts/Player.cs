using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody rb;
    private Vector3 up = new Vector3(1.0f, 0.0f, 1.0f);
    private Vector3 right = new Vector3(1.0f, 0.0f, -1.0f);
    private float thrust = 20.0f;
    private float friction = 0.9f;
    
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
        if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(up * thrust);
        }
        if (Input.GetKey(KeyCode.S)) {
            rb.AddForce(-up * thrust);
        }
        if (Input.GetKey(KeyCode.A)) {
            rb.AddForce(-right * thrust);
        }
        if (Input.GetKey(KeyCode.D)) {
            rb.AddForce(right * thrust);
        }
    }

    private void Friction() {
        Vector3 vel = rb.velocity;
        vel *= friction;
        rb.velocity = vel;
    }

    void FixedUpdate() {
        Movement();
        Friction();
    }
}
