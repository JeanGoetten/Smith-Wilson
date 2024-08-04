using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    private AudioSource au;
    public List<AudioClip> clips; 

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

        au = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (chasingPlayer && player != null)
        {
            Debug.Log(player); 
            ChasePlayer();
        }
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            yield return MoveToPosition(pointC, patrolSpeed);
            yield return new WaitForSeconds(waitTime);
            yield return MoveToPosition(pointB, patrolSpeed);
            yield return new WaitForSeconds(waitTime);
            yield return LookAround();
            yield return MoveToPosition(pointA, patrolSpeed);
            yield return new WaitForSeconds(waitTime);
            yield return MoveToPosition(pointB, patrolSpeed);
            yield return new WaitForSeconds(waitTime);
            yield return LookAround();
        }
    }

    private IEnumerator MoveToPosition(Vector3 target, float speed)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f && !chasingPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
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
            au.PlayOneShot(clips[0]); 
            chasingPlayer = true;
            StopAllCoroutines(); 
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
        Vector3 target = pointA;
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, patrolSpeed * Time.deltaTime);
            Vector3 direction = (target - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
            yield return null;
        }
        transform.position = target;
        StartCoroutine(Patrol());
    }
}
