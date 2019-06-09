using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NPCMovement : MonoBehaviour
{
    Animator anim;

    public float staticMovement = 1f;
    float stopped = 1;

    Vector2 target;
    public float speed = 1200f;
    public float nextWaypointDistance = 0.3f, walkingSpeed = 0.2f;
    Path path;
    public int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    float waitTime, chanceOfStopping = 0.3f;

    [HideInInspector]
    public Seeker seeker;
    [HideInInspector]
    public Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void GetTarget()
    {
        float gridXhalf = Grid.gridSize.x / 2f;
        float gridYhalf = Grid.gridSize.y / 2f;
        float _xPos = Random.Range(gridXhalf * -1f, gridXhalf);
        float _yPos = Random.Range(gridYhalf * -1f, gridYhalf);
        target = new Vector2(_xPos, _yPos);
        seeker.StartPath(rb.position, target, OnPathCalculated);
    }

    public void OnPathCalculated(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
            waitTime = 0;
        }
        else
        {
            GetTarget();
        }
    }

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("Stop");

        GetTarget();
    }

    void Update()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            GetTarget();
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        var dir = path.vectorPath[currentWaypoint] - transform.position;
        var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime * staticMovement * stopped;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
            currentWaypoint++;

        if (rb.velocity.magnitude >= walkingSpeed)
            anim.SetBool("IsWalking", true);
        else
        {
            anim.SetBool("IsWalking", false);
            if(!isStopped)
                waitTime += Time.deltaTime;

            if (waitTime > 2f)
            {
                GetTarget();
            }
        }
    }

    bool isStopped;
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(5, 16));
        if (Random.value <= chanceOfStopping)
        {
            isStopped = true;
            stopped = 0;
        }
        else
        {
            isStopped = false;
            stopped = 1;
        }
    }
}
