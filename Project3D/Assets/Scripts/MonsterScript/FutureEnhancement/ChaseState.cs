using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{

    private NavMeshAgent enemyObject;
    private GameObject player;

    public ProwlingState prowlingState;

    public int range = 5;

    bool playerOutOfArea = false;

    public override State RunCurrentState()
    {
        if (playerOutOfArea == true)
        {
            return prowlingState;
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

    private void Awake()
    {
        enemyObject = GetComponent<NavMeshAgent>();
        InvokeRepeating("UpdateTarget", 0f, 1f);
    }

    private void UpdateTarget()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (player.tag == "Player" && distanceToPlayer <= range)
        {
            MoveToPlayer();
        }
        else
        {
            playerOutOfArea = true;
        }
    }

    private void MoveToPlayer()
    {
        enemyObject.destination = player.transform.position;
    }

}
