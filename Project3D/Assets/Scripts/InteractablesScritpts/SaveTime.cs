using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTime : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private bool playerNearby = false;
    public float detectionRange = 1.75f;

    public GameTime gameTime;

    private void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= detectionRange)
        {
            playerNearby = true;
        }
        else
        {
            playerNearby = false;
        }
        if (playerNearby == true && Input.GetKeyUp(KeyCode.E))
        {
            gameTime.StopGameTime();
        }
    }
}
