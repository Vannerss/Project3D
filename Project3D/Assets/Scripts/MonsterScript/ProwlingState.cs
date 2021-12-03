using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProwlingState : MonoBehaviour
{
    [SerializeField] private Transform[] walkPoints;

    private NavMeshAgent enemyObject;

    int randWalkPoint;

    float waitTimer;

    bool walkpointSelected = true;


    private void Awake()
    {
        enemyObject = GetComponent<NavMeshAgent>();
        randWalkPoint = Random.Range(0, walkPoints.Length);
    }

    private void Update()
    {
        if (walkpointSelected == true)
        {
            enemyObject.destination = walkPoints[randWalkPoint].position;
        }
    }

    private void OnTriggerEnter(Collider areaCollisioned)
    {

        if (areaCollisioned.gameObject.tag == "Walk Point")
        {
            //StartCoroutine((IEnumerator)WalkPointWaitTimer(waitTimer));
            randWalkPoint = Random.Range(0, walkPoints.Length);
        }
    }

    /*
    public IEnumerable WalkPointWaitTimer(float waitTimer = 10)
    {
            yield return new WaitForSeconds(1.0f);
    }

     * currCountdownValue = countdownValue;
     while (currCountdownValue > 0)
     {
         Debug.Log("Countdown: " + currCountdownValue);
         yield return new WaitForSeconds(1.0f);
         currCountdownValue--;
     }
    */
}
