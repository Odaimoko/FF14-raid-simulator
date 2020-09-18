using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRingEffect : MonoBehaviour
{
    private float[] ringScale = { 1.2f, 1.6f, 2f };
    [SerializeField]
    private GameObject iceOnRingObj;
    private const int numIceOnRing = 6;
    // Start is called before the first frame update
    void Start()
    {
        GenerateIce();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateIce()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        float iceInterval = 2 * Mathf.PI / numIceOnRing;
        float startAngle = Random.Range(0, iceInterval);
        float ringOffset = iceInterval;
        int j_circle = 0;
        foreach (SpriteRenderer item in spriteRenderers)
        {
            if (item.name.Contains("IceRing"))
            { 
                float radius = item.bounds.extents[0];
                Vector3 center = item.bounds.center;
                float interRingOffset = Random.Range(0, Mathf.PI);
                for (int i = 0; i < numIceOnRing; i++)
                {
                    float angle = startAngle + iceInterval * i + interRingOffset;
                    Vector3 icePos = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
                    // Debug.Log(icePos);
                    GameObject ice = Instantiate(iceOnRingObj, icePos, iceOnRingObj.transform.rotation);

                    ice.transform.SetParent(item.transform);
                    Orbit orbit = ice.GetComponent<Orbit>();
                    orbit.Init(center, radius, Orbit.Plane.XZ, angle);
                }
                j_circle += 1;
            }
        }
    }

    void EliminateRings()
    {

    }

    void Hit()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject);
        Debug.Log(other);
    }
}
