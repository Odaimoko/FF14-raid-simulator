using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    // private LineRenderer lineRenderer;
    private MeshFilter theMeshFilter;
    private Mesh theMesh;
    // Start is called before the first frame update
    void Start()
    {
        // lineRenderer = GetComponent<LineRenderer>();
        theMeshFilter = GetComponent<MeshFilter>();
        theMesh = new Mesh();
        theMeshFilter.mesh = theMesh;
        Debug.Log(theMeshFilter);
        InitMesh();
    }
    void InitMesh()
    {
        theMesh.name = "two triangles";
        Vector3[] verts = new Vector3[4]{
            new Vector3(Random.Range(-5,5),Random.Range(-5,5),Random.Range(-5,5)),
            new Vector3(Random.Range(-5,5),Random.Range(-5,5),Random.Range(-5,5)),
            new Vector3(Random.Range(-5,5),Random.Range(-5,5),Random.Range(-5,5)),
            new Vector3(Random.Range(-5,5),Random.Range(-5,5),Random.Range(-5,5)),
        };
        theMesh.vertices = verts;
        const int num_triangles = 2;
        int[] tri = new int[num_triangles * 3]{
            0,1,3,
            3,2,1
        };
        theMesh.triangles = tri;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
