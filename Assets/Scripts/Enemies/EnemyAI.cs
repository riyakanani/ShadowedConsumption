// For vector pathfinding using A* Pathfinding Project
// Tutorial reference: https://www.youtube.com/watch?v=jvtFUfJ6CP8

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public Transform Shadow;

    private Transform target;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        FindPlayer();

        // Only start path updates if player was found
        if (target != null)
        {
            InvokeRepeating("UpdatePath", 0f, 0.5f);
        }
    }

    void FindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
    }

    void UpdatePath()
    {
        if (target == null)
        {
            FindPlayer();
            return;
        }

        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        // Keep trying to find the player if missing
        if (target == null)
        {
            FindPlayer();
            return;
        }

        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Flip shadow based on direction
        if (force.x >= 0.01f)
        {
            Shadow.localScale = new Vector3(Mathf.Abs(Shadow.localScale.x), Shadow.localScale.y, Shadow.localScale.z);
        }
        else if (force.x <= -0.01f)
        {
            Shadow.localScale = new Vector3(-Mathf.Abs(Shadow.localScale.x), Shadow.localScale.y, Shadow.localScale.z);
        }
    }
}
