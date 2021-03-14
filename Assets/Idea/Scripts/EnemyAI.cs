using System.Collections;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Seeker))]

public class EnemyAI : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private float updateRate = 2f;
    [SerializeField] private float speedPerSecond = 300f;
    [SerializeField] private float nextWaypointDistance = 3;
    [SerializeField] private GameObject deathVisualEffect;
    [SerializeField] private ForceMode2D forceMode;

    private Seeker seeker;
    private Rigidbody2D rb;
    private Path path;
    private bool pathIsEnded = false;
    private float nextTimeToSearch = 0f;
    private float searchDelay = 0.5f;
    private int currentWaypoint = 0;

    private void Awake() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        if (target != null) StartCoroutine(UpdatePath());
    }

    private void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

    private IEnumerator UpdatePath() {
        if (target == null) {
            SearchForPlayer();
            yield break;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    private void FixedUpdate() {
        if (target == null) {
            SearchForPlayer();
            return;
        }

        if (path == null) return;

        bool isEndOfPath = currentWaypoint >= path.vectorPath.Count;

        if (isEndOfPath && pathIsEnded) return;
        if (isEndOfPath) {
            pathIsEnded = true;
            return;
        }

        pathIsEnded = false;

        Vector3 currentPath = path.vectorPath[currentWaypoint];
        Vector3 direction = (currentPath - transform.position).normalized * speedPerSecond * Time.fixedDeltaTime;
        float distance = Vector3.Distance(transform.position, currentPath);

        rb.AddForce(direction, forceMode);

        if (distance < nextWaypointDistance) currentWaypoint += 1;
    }

    private void Die() {
        Instantiate(deathVisualEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void SearchForPlayer() {
        if (nextTimeToSearch <= Time.time) {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null) {
                target = player.transform;
                StartCoroutine(UpdatePath());
            }

            nextTimeToSearch = Time.time + searchDelay;
        }
    }
}
