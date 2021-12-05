using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{

    private NavMeshAgent enemyObject;
    private GameObject player;

    private int speed = 2;
    public int range = 5;

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
            Debug.Log("Enemy out of range");
        }
    }

    private void MoveToPlayer()
    {
        //enemyObject.transform.position = Vector3.MoveTowards(enemyObject.transform.position, player.transform.position, 1); 
        enemyObject.destination = player.transform.position;
    }

    public override State RunCurrentState()
    {
        throw new System.NotImplementedException();
    }

}
