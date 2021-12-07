using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    private PlayerStats _playerStats;
    private InputHandler _input;
    private Transform _enemyTransform;

    [SerializeField]

    private float moveSpeed = 0;
    public float detectionArea = 0;

    [SerializeField]

    private Camera PlayerCamera;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<InputHandler>();
        _playerStats = GetComponent<PlayerStats>();
        InvokeRepeating("AreaDetector", 0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

        MovetowardsTarget(targetVector);
        AreaDetector();
        RotateTowardMouseVector();
    }

    private void RotateTowardMouseVector()
    {
        Ray ray = PlayerCamera.ScreenPointToRay(_input.MousePosition);

        if(Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private void MovetowardsTarget(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime;
        targetVector = Quaternion.Euler(0, PlayerCamera.gameObject.transform.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
    }

    private void AreaDetector()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
            if (nearestEnemy != null && shortestDistance <= detectionArea)
            {
                _playerStats.SetHealth(-1);
            }
        }
    }


}
