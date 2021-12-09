using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private GameObject doorObject;
    [SerializeField] private Transform playerTransform;

    public GameObject pressEnter;
    public GameObject missingKeys;

    public KeyHolder playerKeys;
    
    public List<Key.KeyType> keyList;

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

                if (playerKeys.ContainsKeys(keyList[0], keyList[1], keyList[2]))
                {
                    pressEnter.SetActive(true);
                    if (playerNearby == true && Input.GetKeyUp(KeyCode.E))
                    {
                        door.ToggleDoor();
                        pressEnter.SetActive(false);
                        wasInteracted = true;
                    }
                }
                else
                {
                    missingKeys.SetActive(true);

                }
            }
            else
            {
                missingKeys.SetActive(false);
                pressEnter.SetActive(false);
            }
        }
    }
}
