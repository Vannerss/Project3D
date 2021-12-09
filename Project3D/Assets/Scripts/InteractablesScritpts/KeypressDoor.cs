using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypressDoor : MonoBehaviour
{
    [SerializeField] private GameObject doorObject;
    [SerializeField] private Transform playerTransform;
    public GameObject pressEInteract;

    private DoorBasics door;
    private bool playerNearby = false;
    public float detectionRange = 1.75f;
    private bool wasInteracted = false;

    private void Awake()
    {
        door = doorObject.GetComponent<DoorBasics>();
    }

    private void Update()
    {
        if (!wasInteracted)
        {
            if (Vector3.Distance(transform.position, playerTransform.position) <= detectionRange)
            {
                playerNearby = true;
                pressEInteract.SetActive(true);
            }
            else
            {
                playerNearby = false;
                pressEInteract.SetActive(false);
            }
            if (playerNearby == true && Input.GetKeyUp(KeyCode.E))
            {
                door.ToggleDoor();
                pressEInteract.SetActive(false);
                wasInteracted = true;
            }
        }
    }
}
