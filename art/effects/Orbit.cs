using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public enum Plane
    {
        XY, XZ, YZ
    }
    private Vector3 centroid;
    private float currentAngle;
    private Plane plane;
    private float radius;
    [SerializeField]
    private float angleSpeed = 3f;
    public float selfRotateSpeed = 0;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Init(Vector3 c, float r, Plane p, float angle)
    {
        centroid = c;
        radius = r;
        plane = p;
        currentAngle = angle;
    }
    // Update is called once per frame
    void Update()
    {
        float newAngle = currentAngle + Time.deltaTime * angleSpeed;
        Vector3 newPos;
        if (plane == Plane.XY)
        {
            newPos = new Vector3(Mathf.Cos(newAngle), Mathf.Sin(newAngle), 0) * radius + centroid;
        }
        else if (plane == Plane.XZ)
        {
            newPos = new Vector3(Mathf.Cos(newAngle), 0, Mathf.Sin(newAngle)) * radius + centroid;
        }
        else
        {
            newPos = new Vector3(0, Mathf.Cos(newAngle), Mathf.Sin(newAngle)) * radius + centroid;
        }
        currentAngle = newAngle;
        gameObject.transform.position = newPos;
    }
}
