using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public GameObject GameManager;
    public Transform player;
    public Rigidbody rb;
    public float speed = 3f;
    public float vision = 10f;
    public float stunRemaining = 0f;
    public float stunDuration = 5f;
    public float wanderRadius = 20f;
    private Vector3 randomDestination;
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
            nav.enabled = false;
        } // Move towards player if within radius
        else if (Vector3.Distance(this.transform.position, player.position) < vision)
        {
            nav.SetDestination(player.position);
        } // Player isn't within range, generate random position and move towards it
        else if (randomDestination == null || Vector3.Distance(this.transform.position, randomDestination) < 1f)
        {
            randomDestination = RandomNavmeshLocation(wanderRadius);
            nav.SetDestination(randomDestination);
        }
    }

    public void stun(Vector3 push)
    {
        stunRemaining = stunDuration;
        rb.AddForce(push);
    }

    private Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius + transform.position;
        UnityEngine.AI.NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

}
