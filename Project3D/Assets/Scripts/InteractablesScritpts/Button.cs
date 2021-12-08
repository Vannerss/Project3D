using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject Timer;
    [SerializeField] private Transform playerTransform;
    private Timer time;
    private bool playerNearby;
    public float detectionRange = 1.75f;

    private void Awake()
    {
        time = Timer.GetComponent<Timer>();
    }

    private void Update()
    {
        playerNearby = false;
        if (Vector3.Distance(transform.position, playerTransform.position) <= detectionRange)
        {
            playerNearby = true;
        }
        if (playerNearby == true && Input.GetKeyUp(KeyCode.E))
        {
            time.StartTimer();
        }
    }
}
