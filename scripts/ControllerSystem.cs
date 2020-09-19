using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Status))]
public class ControllerSystem : MonoBehaviour
{
    // For keyboard and mouse, console and mobile
    public Camera mainCam;
    [SerializeField] private float moveSpeed = 4f, spinSpeed = .4f;
    private Rigidbody playerRB;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
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
        if (inputVec.magnitude > 0.1)
            MoveByWorldVector(inputVec);


        ChangeAnimation();
    }

    void MoveByWorldVector(Vector3 inputVec)
    {
        Vector3 camForward = GetCamForward();
        float camZAngle = Vector3.Angle(Vector3.forward, camForward) / 180 * Mathf.PI;
        if (camForward.x > 0) camZAngle = Mathf.PI * 2 - camZAngle;
        Vector3 worldTowards = new Vector3(
            Mathf.Cos(camZAngle) * inputVec.x - Mathf.Sin(camZAngle) * inputVec.z,
            0,
            Mathf.Sin(camZAngle) * inputVec.x + Mathf.Cos(camZAngle) * inputVec.z);
        transform.Translate(worldTowards.normalized * moveSpeed * Time.deltaTime, Space.World);
        // rotate towards the velocity direction
        float towardsZAngle = Vector3.Angle(Vector3.forward, worldTowards);
        // rotation property is based on rotating around the axis clockwise
        if (worldTowards.x < 0) towardsZAngle = 360 - towardsZAngle;
        Debug.Log("towardsZAngle");
        Debug.Log(towardsZAngle);
        Debug.Log("transform.eulerAngles");
        Debug.Log(transform.eulerAngles);
        Debug.Log(towardsZAngle - transform.eulerAngles.y);
        float rotationY = transform.eulerAngles.y;
        if (Mathf.Abs(towardsZAngle - transform.eulerAngles.y) > 2f)
        {
            float rotateDirection;
            if (towardsZAngle - rotationY > 180)
                rotateDirection = towardsZAngle - rotationY - 360;
            else if (towardsZAngle - rotationY < -180)
                rotateDirection = towardsZAngle - rotationY + 360;
            else
                rotateDirection = towardsZAngle - rotationY;
            transform.Rotate(
                0,
                spinSpeed * rotateDirection,
                0);
        }
    }

    void ChangeAnimation()
    {
    }

    Vector2 GetKeyBoardInput()
    {
        Vector2 input = new Vector2(0, 0);
        return input;
    }
}
