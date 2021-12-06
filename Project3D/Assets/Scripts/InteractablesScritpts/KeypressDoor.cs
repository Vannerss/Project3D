using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypressDoor : MonoBehaviour
{
    [SerializeField] private GameObject doorObject;
    [SerializeField] private Transform playerTransform;
    private DoorBasics door;
    private bool playerNearby;
    public float detectionRange = 1.75f;

    private void Awake()
    {
        door = doorObject.GetComponent<DoorBasics>();
    }

    private void Update()
    {
        playerNearby = false;
        if(Vector3.Distance(transform.position, playerTransform.position) <= detectionRange)
        {
            playerNearby = true;
        }
        if (playerNearby == true && Input.GetKeyUp(KeyCode.E) )
        {
            door.ToggleDoor();  
        }
    }
}
