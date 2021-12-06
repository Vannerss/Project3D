using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (FieldofView))]
public class Showfieldofview : Editor
{
    //NO TOCAR DEMUESTRA EL FIELD OF FIELD OF VIEW
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnSceneGUI()
    {
        FieldofView fow = (FieldofView)target;
        Handles.color = Color.white;
        Vector3 viewAngleA = fow.DirectionfromAngle(-fow.viewangle / 2, false);
        Vector3 viewAngleB = fow.DirectionfromAngle(fow.viewangle / 2, false);
        Handles.DrawWireArc(fow.transform.position, Vector3.up, viewAngleA, fow.viewangle, fow.viewradius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewradius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewradius);
    }
}
