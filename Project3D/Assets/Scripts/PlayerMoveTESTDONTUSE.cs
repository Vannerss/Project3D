using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveTESTDONTUSE : MonoBehaviour
{
    //NO SE USA, ESTO ES PARA TEST PURPOSES
    public Rigidbody playerrigidbody;
    public float speed;
    public float playerx;
    public float playery;
    Vector3 movedirection;
    // Start is called before the first frame update
    void Start()
    {
        playerrigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        playerx = Input.GetAxis("Horizontal");
        playery = Input.GetAxis("Vertical");
        movedirection = transform.forward * playery + transform.right * playerx;
    }
    void MovePlayer()
    {
        playerrigidbody.AddForce(movedirection.normalized * speed, ForceMode.Acceleration);
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
}
