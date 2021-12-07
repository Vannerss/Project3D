using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProwlingState : State 
{
    [SerializeField] private Transform[] walkPoints;

    private NavMeshAgent enemyObject;
    private GameObject player;

    public ChaseState chaseState;
    public int range = 5;

    int previousWalkPoint;
    int randWalkPoint;

    float waitTimer;
    float currCountdownValue;

    bool walkpointSelected = false;
    bool timerWalkPointStoped = false;
    bool playerInAreaDetected = false;

    public override State RunCurrentState()
    {
        if (playerInAreaDetected == true)
        {
            return chaseState;
        }
        else
        {
            return this;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void UpdateTarget()
    {
        Debug.Log("StateMachine works");
        player = GameObject.FindGameObjectWithTag("Player");
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (player.tag == "Player" && distanceToPlayer <= range)
        {
            playerInAreaDetected = true;
        }
    }

    private void WalkPointSelector()
    {
        Debug.Log("ProwlingState");
        while (walkpointSelected == false)
        {
            randWalkPoint = Random.Range(0, walkPoints.Length);
            if (previousWalkPoint != randWalkPoint)
            {
             
                walkpointSelected = true;
            }
            previousWalkPoint = randWalkPoint;
        }

        enemyObject.destination = walkPoints[randWalkPoint].position;
    }

    private void Awake()
    {
        enemyObject = GetComponent<NavMeshAgent>();
        WalkPointSelector();
    }

    private void OnTriggerEnter(Collider areaCollisioned)
    {

        if (areaCollisioned.gameObject.tag == "Walk Point")
        {
            StartCoroutine((IEnumerator)StartCountdown(3));
        }
    }

    public IEnumerator StartCountdown(float countdownValue)
    {
        currCountdownValue = countdownValue;
        if (timerWalkPointStoped == false)
        {
            timerWalkPointStoped = true;
            while (currCountdownValue > 0)
            {
                //Debug.Log("Countdown: " + currCountdownValue);
                yield return new WaitForSeconds(1.0f);
                currCountdownValue--;
            }
            if (currCountdownValue <= 0)
            {
                timerWalkPointStoped = false;
                walkpointSelected = false;
                WalkPointSelector();
            }
        }
    }
}
