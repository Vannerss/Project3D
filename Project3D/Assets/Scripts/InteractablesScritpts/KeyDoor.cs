using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private GameObject doorObject;
    [SerializeField] private Transform playerTransform;

    public KeyHolder playerKeys;
    
    public List<Key.KeyType> keyList;

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
        if (Vector3.Distance(transform.position, playerTransform.position) <= detectionRange)
        {
            playerNearby = true;
        }
        if (playerNearby == true && Input.GetKeyUp(KeyCode.E))
        {
            if (playerKeys.ContainsKeys(keyList[0], keyList[1], keyList[2]))
            {
                door.ToggleDoor();
            }
        }
    }
}
