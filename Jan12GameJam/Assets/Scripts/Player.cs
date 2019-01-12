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
    public Vector3 carryDisplacement = new Vector3(0.0f, 1.0f, 0.0f);

    public int maxLives = 5;
    private int lives;

    public float staminaRechargeTime = 2.0f;
    private float staminaRechargeTimer;

    public int maxStamina = 3;
    private int stamina;

    public bool isBlocking = false;

    public GameObject carryingObject = null;

    private GameObject collidingCollectable = null;

    public GameObject GetCarryingObject() {
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
        if (stamina >= maxStamina || carryingObject != null) {
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

    public void PerformStun(EnemyMovement em) {
        throw new NotImplementedException();
        ReduceStamina(1);
        StopBlock();
    }

    protected void OnTriggerEnter(Collider collider) {
        GameObject go = collider.gameObject;
        EnemyMovement em = go.GetComponent<EnemyMovement>();
        if (em) {
            if (isBlocking) {
                PerformStun(em);
            }
            else {
                TakeDamage();
            }
            return;
        }

        Collectable collectable = go.GetComponent<Collectable>();
        if (collectable) {
            // TODO this is a hack
            collidingCollectable = go;
            return;
        }
    }

    protected void OnTriggerExit(Collider collider) {
        GameObject go = collider.gameObject;
        Collectable collectable = go.GetComponent<Collectable>();
        if (collectable) {
            // TODO this is a hack
            collidingCollectable = null;
        }
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

        carryingObject.transform.position = rb.position + carryDisplacement;
    }

    private void Actions() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            StartBlock();
        }
        if (Input.GetKeyUp(KeyCode.Z)) {
            StopBlock();
        }
        
        if (Input.GetKeyDown(KeyCode.X)) {
            if (carryingObject != null) {
                DropCarryingObject();
            }
            else {
                if (collidingCollectable != null) {
                    StartCarry(collidingCollectable);
                }
            }
        }
    }

    private void Movement() {
        bool moving = false;
        if (Input.GetKey(KeyCode.UpArrow)) {
            rb.AddForce(up * thrust);
            moving = true;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            rb.AddForce(-up * thrust);
            moving = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            rb.AddForce(-right * thrust);
            moving = true;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            rb.AddForce(right * thrust);
            moving = true;
        }

        if (moving) {
            Quaternion targetRotation = Quaternion.LookRotation(rb.velocity);
            transform.rotation = targetRotation;
        }
    }

    public void StartCarry(GameObject obj) {
        carryingObject = obj;
    }

    public void DropCarryingObject() {
        if (carryingObject != null) {
            Vector3 pos = rb.position;
            pos.y = GameManager.GetCurrent().collectableY;
            carryingObject.transform.position = pos;
            carryingObject = null;
        }
    }

    private void Friction() {
        Vector3 vel = rb.velocity;
        vel *= friction;
        rb.velocity = vel;
    }

    public void Die() {
        Debug.Log("Player Died.");
    }

    public void TeleportToBase() {
        rb.position = GameManager.GetCurrent().spawn.transform.position;
    }

    public void TakeDamage() {
        DropCarryingObject();
        lives--;
        if (lives == 0) {
            Die();
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
