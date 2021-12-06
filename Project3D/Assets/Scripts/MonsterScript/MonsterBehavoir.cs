using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehavoir : MonoBehaviour
{

    [SerializeField] private Transform[] walkPoints;

    private NavMeshAgent enemyObject;
    private GameObject player;

    string currentState;
    string prowlingState = "prowlingState";
    string chaseState = "chaseState";
    string searchState = "searchState";

    public int range = 5;

    int previousWalkPoint;
    int randWalkPoint;

    float waitTimer;
    float currCountdownValue;

    bool walkpointSelected = false;
    bool timerWalkPointStoped = false;
    bool playerInAreaDetected = false;

    //Behavoir Controller 
    private void Awake()
    {
        enemyObject = GetComponent<NavMeshAgent>();
        InvokeRepeating("UpdateTarget", 0f, 1f);
        BehaviorController(prowlingState);
    }

    private void BehaviorController(string State)
    {
        switch (State)
        {
            case "prowlingState":
                //Debug.Log(prowlingState);
                WalkPointSelector();
                break;
            case "chaseState":
                break;
            case "searchState":
                break;
            default:
                Debug.Log("Fuck oof");
                break;

        }
    }

    // Prowling Behaviour 
    private void UpdateTarget()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (player.tag == "Player" && distanceToPlayer <= range)
        {
            Debug.Log("Target in range");
        }
    }

    private void WalkPointSelector()
    {
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

    private void MoveToPlayer()
    {
        enemyObject.destination = player.transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

