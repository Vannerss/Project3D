using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontUseTestCameraMovement : MonoBehaviour
{
    public float camx;
    public float camy;
    public float camsensitivity;
    public Camera camera;
    public float rotatex;
    public float rotatey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camusage();
        camera.transform.localRotation = Quaternion.Euler(rotatex, 0, 0);
        transform.rotation = Quaternion.Euler(0, rotatey, 0);
    }

    public void Camusage()
    {
        camx = Input.GetAxisRaw("Mouse X");
        camy = Input.GetAxisRaw("Mouse Y");

        rotatex -= camy * camsensitivity * 0.01f;
        rotatey += camx * camsensitivity * 0.01f;

        rotatex = Mathf.Clamp(rotatex, -90f, 90f);

    }
}
