using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject Timer;
    [SerializeField] private Transform playerTransform;
    private Timer time;
    public GameObject lastDoor;
    private bool playerNearby = false;
    public float detectionRange = 1.75f;
    public GameObject pressEInteract;
    private bool wasInteracted = false;

    private void Awake()
    {
        time = Timer.GetComponent<Timer>();
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
                time.StartTimer();
                pressEInteract.SetActive(false);
                lastDoor.SetActive(true);
            }
        }
    }
}
