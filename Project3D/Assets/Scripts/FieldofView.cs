using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldofView : MonoBehaviour
{

    public float viewradius;
    public float viewangle;
    public LayerMask objectstoview;
    public LayerMask walls;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }   
    public void FindVisible()
    {
        Collider[] objectsinradius = Physics.OverlapSphere(transform.position, viewradius, objectstoview);
        for (int i = 0; i < objectsinradius.Length; i++)
        {
            Transform objects = objectsinradius[i].transform;
            Vector3 directiontoobjects = (objects.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directiontoobjects)< viewangle/2)
            {
                float disttoobject = Vector3.Distance(transform.position, objects.position);
                if (!Physics.Raycast(transform.position,directiontoobjects,disttoobject,walls))
                {

                }
            }
        }
    }
    public Vector3 DirectionfromAngle(float angle, bool angleisglobal)
    {
        if (!angleisglobal)
        {
            angle += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}
