using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSystem : MonoBehaviour
{
    // For keyboard and mouse, console and mobile
    public Camera mainCam;
    private Animator animator;
    public float moveSpeed
    {
        get => defaultMoveSpeed * moveSpeedMultiplier;
        set { }
    }
    //  sometimes the player cannot control themselves
    public bool controllable { get; set; } = true;
    private SinglePlayer controlledPlayer;

    private float defaultMoveSpeed = 4f;
    // Accerlarate, or slowdown
    public float moveSpeedMultiplier = 1f;

    private float spinSpeed = .4f;
    private Vector2 touchStartPos = Vector2.zero, touchEndPos = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        //  Just control this player's animation 
        controlledPlayer =  gameObject.transform.GetChild(0).GetComponent<SinglePlayer>();
        animator = controlledPlayer.GetComponent<Animator>();
    }

    void Update()
    {
    }

    public void Control()
    {
        if (!controllable)
        {
            ChangeAnimation(false);
        }
        else
        {

            Vector3 inputVec = GetInputVector();
            if (inputVec.magnitude > 0.1)
            {
                MoveByWorldVector(inputVec);
                ChangeAnimation(true);
            }
            else
            {
                ChangeAnimation(false);
            }
        }
    }

    Vector3 GetCamForward()
    {
        Vector3 towards = mainCam.transform.forward;
        towards.y = 0;
        towards = towards.normalized;
        return towards;
    }

    Vector3 GetInputVector()
    {

        Vector3 inputVec = Vector3.zero;
        if (Input.touchCount > 0)
        {
            // For mobile devices
            //  use finger id 0
            Touch t = Input.GetTouch(0);
            switch (t.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = t.position;
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    touchEndPos = t.position;
                    break;
                default:
                    touchEndPos = Vector2.zero;
                    touchStartPos = Vector2.zero;
                    break;
            }
            Vector2 direction = touchEndPos - touchStartPos;
            inputVec = new Vector3(direction.x, 0, direction.y);
        }
        else
        {
            float xInput = Input.GetAxis("Horizontal"), zInput = Input.GetAxis("Vertical");
            inputVec = new Vector3(xInput, 0, zInput);
        }
        return inputVec.normalized;
    }

    void MoveByWorldVector(Vector3 inputVec)
    {
        // assume inputVec is normalized
        Vector3 camForward = GetCamForward();
        float camZAngle = Vector3.Angle(Vector3.forward, camForward) / 180 * Mathf.PI;
        if (camForward.x > 0) camZAngle = Mathf.PI * 2 - camZAngle;
        // rotate the input vector to where the player is facing
        Vector3 worldTowards = new Vector3(
            Mathf.Cos(camZAngle) * inputVec.x - Mathf.Sin(camZAngle) * inputVec.z,
            0,
            Mathf.Sin(camZAngle) * inputVec.x + Mathf.Cos(camZAngle) * inputVec.z);
        transform.Translate(worldTowards.normalized * moveSpeed * Time.deltaTime, Space.World);
        // rotate towards the velocity direction
        float towardsZAngle = Vector3.Angle(Vector3.forward, worldTowards);
        // rotation in unity is clockwise, based on controlled player
        if (worldTowards.x < 0) towardsZAngle = 360 - towardsZAngle;
        float rotationY = controlledPlayer.transform.eulerAngles.y;
        if (Mathf.Abs(towardsZAngle - controlledPlayer.transform.eulerAngles.y) > 2f)
        {
            float rotateDirection;
            if (towardsZAngle - rotationY > 180)
                rotateDirection = towardsZAngle - rotationY - 360;
            else if (towardsZAngle - rotationY < -180)
                rotateDirection = towardsZAngle - rotationY + 360;
            else
                rotateDirection = towardsZAngle - rotationY;
            controlledPlayer.transform.Rotate(
                 0, spinSpeed * rotateDirection, 0);
        }
    }

    public void ChangeAnimation(bool move)
    {
        if (move)
        {
            if (moveSpeedMultiplier > .5)
            {
                animator.SetBool("Run", true);
                animator.SetBool("Walk", false);
            }
            else
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
            }
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);

        }
    }

    Vector2 GetKeyBoardInput()
    {
        Vector2 input = new Vector2(0, 0);
        return input;
    }
}
