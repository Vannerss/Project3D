using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProwlingState : State 
{
    [SerializeField] private Transform[] walkPoints;

    private NavMeshAgent enemyObject;

    int previousWalkPoint;
    int randWalkPoint;
    public int range = 5;

    float waitTimer;
    float currCountdownValue;

    bool walkpointSelected = false;
    bool startWalkPointSelected = false;
    bool timerWalkPointStoped = false;

    public override State RunCurrentState()
    {
        throw new System.NotImplementedException();
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }*/

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
                Debug.Log("Countdown: " + currCountdownValue);
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