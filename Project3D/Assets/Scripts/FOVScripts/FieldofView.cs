using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldofView : MonoBehaviour
{
    private MonsterBehavoir monsterBehavoir;

    public float MaskCutawaydistance = 0.1f;
    public float viewradius;
    public float viewangle;
    public LayerMask objectstoview;
    public LayerMask wallslayer;
    public bool isflashlightoff =false;
    public MeshFilter viewmeshfilter;
    Mesh viewmesh;


    public float edgedistancethreshold;
    public int EdgeResolveIterations;

    public List<Transform> visibletargets = new List<Transform>();
    public float meshresolution;
    // Start is called before the first frame update
    void Start()
    {
        monsterBehavoir = GetComponent<MonsterBehavoir>();
        viewmesh = new Mesh();
        viewmesh.name = "View Mesh";
        viewmeshfilter.mesh = viewmesh;


        StartCoroutine("Findtargetswithdelay", 0.2f);
    }
     void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {

            if (isflashlightoff == false)
            {
                isflashlightoff = true;
                monsterBehavoir.ReduceDetectionRange(isflashlightoff);
            }
            else
            {
                isflashlightoff = false;
                monsterBehavoir.ReduceDetectionRange(isflashlightoff);
            }
        }


    }

    // Update is called once per frame
    void LateUpdate()
    {
        DrawFieldofView();
    }   

    public void FindVisible()
    {
        visibletargets.Clear();
        Collider[] objectsinradius = Physics.OverlapSphere(transform.position, viewradius, objectstoview);
        for (int i = 0; i < objectsinradius.Length; i++)
        {
            Transform objects = objectsinradius[i].transform;
            Vector3 directiontoobjects = (objects.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directiontoobjects)< viewangle/2)
            {
                float disttoobject = Vector3.Distance(transform.position, objects.position);
                if (!Physics.Raycast(transform.position,directiontoobjects,disttoobject,wallslayer))
                {
                    visibletargets.Add(objects);
                }
            }
        }
    }

    IEnumerator Findtargetswithdelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisible();
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
    void DrawFieldofView()
    {
        int stepcount = Mathf.RoundToInt(viewangle * meshresolution);
        float stepanglesize = viewangle / stepcount;
        List<Vector3> viewpoints = new List<Vector3>();
        ViewCastInfo oldviewcast = new ViewCastInfo();
        if (isflashlightoff==false)
        {
            for (int i = 0; i < stepcount; i++)
            {
                float angle = transform.eulerAngles.y - viewangle / 2 + stepanglesize * i;
                //Debug.DrawLine(transform.position, transform.position + DirectionfromAngle(angle, true) * viewradius, Color.red);
                ViewCastInfo newViewCast = ViewCast(angle);
                if (i > 0)
                {
                    bool edgedistancethresholdexceeded = Mathf.Abs(oldviewcast.distance - newViewCast.distance) > edgedistancethreshold;
                    if (oldviewcast.hit != newViewCast.hit || (oldviewcast.hit && newViewCast.hit && edgedistancethresholdexceeded))
                    {
                        Edgeinfo Edge = FindEdge(oldviewcast, newViewCast);

                        if (Edge.pointA != Vector3.zero)
                        {
                            viewpoints.Add(Edge.pointA);
                        }
                        if (Edge.pointB != Vector3.zero)
                        {
                            viewpoints.Add(Edge.pointB);
                        }
                    }
                }

                viewpoints.Add(newViewCast.point);
                oldviewcast = newViewCast;
            }

            int vertexcount = viewpoints.Count + 1;
            Vector3[] vertices = new Vector3[vertexcount];
            int[] triangles = new int[(vertexcount - 2) * 3];
            vertices[0] = Vector3.zero;

            for (int i = 0; i < vertexcount - 1; i++)
            {
                if (i < vertexcount - 2)
                {
                    vertices[i + 1] = transform.InverseTransformPoint(viewpoints[i]) + Vector3.forward * MaskCutawaydistance;
                    triangles[i * 3] = 0;
                    triangles[i * 3 + 1] = i + 1;
                    triangles[i * 3 + 2] = i + 2;
                }
            }
            viewmesh.Clear();
            viewmesh.vertices = vertices;
            viewmesh.triangles = triangles;
            viewmesh.RecalculateNormals();
        }
        else
        {
            viewmesh.Clear();
        }
    }
       

    ViewCastInfo ViewCast(float globalangle)
    {
        Vector3 direction = DirectionfromAngle(globalangle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, viewradius, wallslayer))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalangle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + direction * viewradius, viewradius, globalangle);
        }
    }

    Edgeinfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;

        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;
        for (int i = 0; i < EdgeResolveIterations; i++)
        {

            float angle = minAngle + maxAngle / 2;
            ViewCastInfo newViewCast = ViewCast(angle);
            bool edgedistancethresholdexceeded = Mathf.Abs(minViewCast.distance - newViewCast.distance) > edgedistancethreshold;
            if (newViewCast.hit == minViewCast.hit && !edgedistancethresholdexceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }
        return new Edgeinfo(minPoint, maxPoint);
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float distance;
        public float angle;

        public ViewCastInfo (bool bhit, Vector3 vpoint, float fdistance, float fangle)
        {
            hit = bhit;
            point = vpoint;
            distance = fdistance;
            angle = fangle;
        }
    }

    public struct Edgeinfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public Edgeinfo(Vector3 vpointA, Vector3 vpointB)
        {
            pointA = vpointA;
            pointB = vpointB;
        }
    }

    public void TurnoffFlashlight()
    {
            viewradius = 0;
            viewangle = 0;         
    }
    public void TurnonFlashlight()
    {
        viewradius = 37;
        viewangle = 70;
    }


}
