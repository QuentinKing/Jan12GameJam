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

    public int maxLives = 5;
    private int lives;

    public float staminaRechargeTime = 2.0f;
    private float staminaRechargeTimer;

    public int maxStamina = 3;
    private int stamina;

    public bool isBlocking = false;

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
        
        lives = maxLives;
        stamina = maxStamina;
        staminaRechargeTimer = 0.0f;
        isBlocking = false;
    }

    // Update is called once per frame
    protected void Update()
    {
        Actions();
        UpdateStamina();
    }

    private void UpdateStamina() {
        if (stamina >= maxStamina) {
            return;
        }
        
        if (staminaRechargeTimer >= staminaRechargeTime) {
            staminaRechargeTimer = 0f;
            stamina++;
        }
        staminaRechargeTimer += Time.deltaTime;
    }

    public int GetStamina() {
        return stamina;
    }

    public int GetMaxStamina() {
        return maxStamina;
    }

    public void ReduceStamina(int amount) {
        stamina -= amount;
        if (stamina < 0) stamina = 0;
        staminaRechargeTimer = 0.0f;
    }

    public int GetLives() {
        return lives;
    }

    public int GetMaxLives() {
        return maxLives;
    }

    public bool IsBlocking() {
        return isBlocking;
    }

    public void StartBlock() {
        if (isBlocking || GetStamina() <= 0) {
            return;
        }

        isBlocking = true;
    }

    public void PerformStun() {
        throw new NotImplementedException();
        ReduceStamina(1);
        StopBlock();
    }

    public void StopBlock() {
        if (!isBlocking) {
            return;
        }
        
        isBlocking = false;
    }

    private void UpdateCarryingObject() {
        if (carryingObject == null) {
            return;
        }

        carryingObject.isKinematic = false;
        carryingObject.position = rb.position + carryDisplacement;
    }

    private void Actions() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            StartBlock();
        }
        if (Input.GetKeyUp(KeyCode.Z)) {
            StopBlock();
        }
    }

    private void Movement() {
        if (Input.GetKey(KeyCode.UpArrow)) {
            rb.AddForce(up * thrust);
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            rb.AddForce(-up * thrust);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            rb.AddForce(-right * thrust);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
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
