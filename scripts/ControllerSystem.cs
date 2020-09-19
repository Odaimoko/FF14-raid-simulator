using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSystem : MonoBehaviour
{
    // For keyboard and mouse, console and mobile
    public Camera mainCam;
    [SerializeField] private float moveSpeed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    Vector3 GetCamForward()
    {

        Vector3 towards = mainCam.transform.forward;
        towards.y = 0;
        towards = towards.normalized;
        return towards;

    }

    void Update()
    {
        float xInput = Input.GetAxis("Horizontal"), zInput = Input.GetAxis("Vertical");
        Vector3 inputVec = new Vector3(xInput, 0, zInput);
        MoveByWorldVector(inputVec);

    }

    void MoveByWorldVector(Vector3 inputVec)
    {

        Vector3 camForward = GetCamForward();
        float camZAngle = Vector3.Angle(Vector3.forward, camForward) / 180 * Mathf.PI;
        if (camForward.x > 0)
        {
            camZAngle = Mathf.PI * 2 - camZAngle;
        }
        Vector3 worldTowards = new Vector3(Mathf.Cos(camZAngle) * inputVec.x - Mathf.Sin(camZAngle) * inputVec.z, 0,
        Mathf.Sin(camZAngle) * inputVec.x + Mathf.Cos(camZAngle) * inputVec.z);
        transform.Translate(worldTowards.normalized * moveSpeed * Time.deltaTime);
    }

    Vector2 GetKeyBoardInput()
    {
        Vector2 input = new Vector2(0, 0);
        return input;
    }
}
