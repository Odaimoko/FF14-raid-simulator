using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    // Controls camera and audio
    // Start is called before the first frame update
    [SerializeField]
    private float maxFocalDistance, minFocalDistance, rotateSensitivity;
    public GameObject player;
    public Camera mainCam;
    [SerializeField]
    private bool followPlayer;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        if (followPlayer)
            transform.position = player.transform.position;
    }

    void RotateCamera(float angle)
    {

    }
    void MoveTowardsFocalPoint()
    {

    }
    void getForwardVector()
    {

    }
}
