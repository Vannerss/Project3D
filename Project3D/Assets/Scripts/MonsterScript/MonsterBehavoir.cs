using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehavoir : MonoBehaviour
{

    [SerializeField] private Transform[] walkPoints;

    private NavMeshAgent enemyObject;
    private GameObject player;

    string currentState = "NULL";
    string previousState;
    string prowlingState = "prowlingState";
    string chaseState = "chaseState";
    string searchState = "searchState";

    public int WaitTime = 0;
    public float DetectionRange = 5;
    public int WalkingSpeed = 0;
    public int ChasingSpeed = 2;

    int previousWalkPoint;
    int randWalkPoint;

    float currCountdownValue;

    bool walkpointSelected = false;
    bool timerWalkPointStoped = false;
    bool targetpointSelected = false;
    bool timerTargetPointStoped= false;

    //Behavoir Controller 
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = prowlingState;
        enemyObject = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        TargetUpdate();
        switch (currentState)
        {
            case "prowlingState":
                enemyObject.speed = WalkingSpeed;
                WalkPointSelector();
                previousState = prowlingState;
                break;
            case "chaseState":
                enemyObject.speed = ChasingSpeed;
                MoveToPlayer();
                previousState = chaseState;
                break;
            case "searchState":
                enemyObject.speed = WalkingSpeed;
                PlayerLastPoint();
                break;

        }
    }

    // Prowling Behaviour 
    private void TargetUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= DetectionRange && previousState == prowlingState)
        {
            currentState = chaseState;
        }
        else if (distanceToPlayer >= DetectionRange && previousState == searchState)
        {
            currentState = prowlingState;
        }
        else if (distanceToPlayer >= DetectionRange && previousState == chaseState)
        {
            currentState = searchState;
        }
        else if (distanceToPlayer <= DetectionRange && previousState == searchState)
        {
            currentState = chaseState;
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
            StartCoroutine((IEnumerator)StartCountdown(WaitTime));
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

    // Chase Player
    private void MoveToPlayer()
    {
        enemyObject.destination = player.transform.position;
    }

    // Search for player 

    private void PlayerLastPoint()
    {
        currentState = searchState;
        if (currentState == searchState)
        {
      
            StartCoroutine((IEnumerator)SearchForPlayer(6));

        }
    }

    private IEnumerator SearchForPlayer(float countdownValue)
    {
        
        float CountdownValue = countdownValue;
        if (timerTargetPointStoped == false)
        {
            timerTargetPointStoped = true;
            while (CountdownValue > 0)
            {
                currentState = searchState;
                yield return new WaitForSeconds(1.0f);
                CountdownValue--;
            }
            if (CountdownValue <= 0)
            {
                currentState = searchState;
                timerTargetPointStoped = false;
                walkpointSelected = false;
                MoveToPlayerLastPosition();
                previousState = searchState;
            }
        }
    }

    private void MoveToPlayerLastPosition()
    {
        enemyObject.destination = player.transform.position;
    }

    public void ReduceDetectionRange(bool flashlightoff)
    {
        if (flashlightoff)
        {
            DetectionRange = DetectionRange / 2;
        }
        else if (flashlightoff)
        {
            DetectionRange = DetectionRange * 2;
        }

    }

    // Gizmo Maker
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }
}

