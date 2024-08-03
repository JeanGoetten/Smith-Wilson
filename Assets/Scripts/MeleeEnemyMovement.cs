using UnityEngine;
using System.Collections;

public class MeleeEnemyMovement : MonoBehaviour
{
    public float patrolSpeed = 2f; 
    public float chaseSpeed = 4f; 
    public float waitTime = 2f; 
    public float lookInterval = 1f; 
    public float patrolLengthX = 5f; 
    public float patrolLengthZ = 5f; 
    public bool patrolAlongX = true; 
    public bool patrolAlongZ = false; 

    private Vector3 initialPosition;
    private Vector3 pointA;
    private Vector3 pointB;
    private Vector3 pointC;
    private bool chasingPlayer = false;
    private Transform player;

    private void Start()
    {
        initialPosition = transform.position;
        pointA = initialPosition;
        pointB = initialPosition;
        pointC = initialPosition;

        if (patrolAlongX)
        {
            pointA -= new Vector3(patrolLengthX / 2, 0, 0);
            pointC += new Vector3(patrolLengthX / 2, 0, 0);
        }
        else if (patrolAlongZ)
        {
            pointA -= new Vector3(0, 0, patrolLengthZ / 2);
            pointC += new Vector3(0, 0, patrolLengthZ / 2);
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Patrol());
    }

    private void Update()
    {
        if (chasingPlayer)
        {
            ChasePlayer();
        }
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            yield return MoveToPosition(pointC);
            yield return new WaitForSeconds(waitTime);
            yield return MoveToPosition(pointB);
            yield return new WaitForSeconds(waitTime);
            yield return LookAround();
            yield return MoveToPosition(pointA);
            yield return new WaitForSeconds(waitTime);
            yield return MoveToPosition(pointB);
            yield return new WaitForSeconds(waitTime);
            yield return LookAround();
        }
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f && !chasingPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, patrolSpeed * Time.deltaTime);
            Vector3 direction = (target - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
            yield return null;
        }
        transform.position = target;
    }

    private IEnumerator LookAround()
    {
        Vector3[] directions = { Vector3.forward, Vector3.right, Vector3.back, Vector3.left };
        foreach (Vector3 dir in directions)
        {
            transform.rotation = Quaternion.LookRotation(dir);
            yield return new WaitForSeconds(lookInterval);
        }
    }

    private void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chasingPlayer = true;
            StopCoroutine(Patrol());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chasingPlayer = false;
            StartCoroutine(ReturnToPatrol());
        }
    }

    private IEnumerator ReturnToPatrol()
    {
        Vector3 target = (Vector3.Distance(transform.position, pointA) < Vector3.Distance(transform.position, pointC)) ? pointA : pointC;
        yield return MoveToPosition(target);
        yield return Patrol();
    }
}
