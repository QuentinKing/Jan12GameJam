using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody rb;
    public Vector3 up = new Vector3(1.0f, 0.0f, 1.0f);
    public Vector3 right = new Vector3(1.0f, 0.0f, -1.0f);
    public float thrust = 20.0f;
    public float friction = 0.9f;
    public Vector3 carryDisplacement = new Vector3(0.0f, 3.0f, 0.0f);

    public int initialLives = 5;
    private int lives;

    public int maxStamina = 5;
    private int stamina;

    public Rigidbody carryingObject = null;
    private bool carryingObjectIsKinematic = false;
    private bool carryingObjectUseGravity = false;

    public Rigidbody GetCarryingObject() {
        return carryingObject;
    }
    
    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        lives = initialLives;
        stamina = maxStamina;
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    public int GetStamina() {
        return stamina;
    }

    public int GetMaxStamina() {
        return maxStamina;
    }

    private void UpdateCarryingObject() {
        if (carryingObject == null) {
            return;
        }

        carryingObject.isKinematic = false;
        carryingObject.position = rb.position + carryDisplacement;
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

    public void StartCarry(Rigidbody obj) {
        carryingObject = obj;
        carryingObjectIsKinematic = obj.isKinematic;
        carryingObjectUseGravity = obj.useGravity;
        obj.isKinematic = false;
        obj.useGravity = false;
    }

    public void Drop() {
        if (carryingObject != null) {
            carryingObject.isKinematic = carryingObjectIsKinematic;
            carryingObject.useGravity = carryingObjectUseGravity;
            carryingObject = null;
        }
    }

    private void Friction() {
        Vector3 vel = rb.velocity;
        vel *= friction;
        rb.velocity = vel;
    }

    public void Die() {
        throw new NotImplementedException();
    }

    public void TeleportToBase() {
        throw new NotImplementedException();
    }

    public void TakeDamage() {
        Drop();
        lives--;
        if (lives == 0) {
            
        }
        else {
            TeleportToBase();
        }
    }

    protected void FixedUpdate() {
        Movement();
        Friction();
        UpdateCarryingObject();
    }
}
