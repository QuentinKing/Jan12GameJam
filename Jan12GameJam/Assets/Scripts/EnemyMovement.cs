using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public GameObject gameManager;
    public Transform player;
    public Rigidbody rb;
    public float speed = 3f;
    public float vision = 7f;
    public float stunRemaining = 0f;
    public float stunDuration = 5f;
    public float wanderRadius = 10f;
    public float wanderRemaining = 0f;
    public float wanderDuration = 5f;
    public Vector3 randomDestination = Vector3.zero;
    private UnityEngine.AI.NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        // Set player from GameManager or in the scene
    }

    // Update is called once per frame
    void Update()
    {
        // Can't move while stunned
        if (stunRemaining > 0)
        {
            stunRemaining = System.Math.Max(stunRemaining - Time.deltaTime, 0);
            nav.enabled = stunRemaining == 0;
        } // Move towards player if within radius
        else if (Vector3.Distance(this.transform.position, player.position) < vision)
        {
            nav.SetDestination(player.position);
        } // Player isn't within range, generate random position and move towards it
        else// if (randomDestination == Vector3.zero || Vector3.Distance(this.transform.position, randomDestination) < 2)
        {
            if (wanderRemaining == 0 || Vector3.Distance(this.transform.position, randomDestination) < 2)
            {
                wanderRemaining = wanderDuration;
                randomDestination = RandomNavmeshLocation(wanderRadius);
                nav.SetDestination(randomDestination);
            } else 
            {
                wanderRemaining = System.Math.Max(wanderRemaining - Time.deltaTime, 0);
            }
        }
    }

    public void stun(Vector3 push)
    {
        stunRemaining = stunDuration;
        rb.AddForce(push);
    }

    private Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;// + transform.position;
        UnityEngine.AI.NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

}
