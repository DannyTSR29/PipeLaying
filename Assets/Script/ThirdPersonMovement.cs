using UnityEngine;
using System.Collections;
using UnityEngine.Assertions.Must;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed;
    //[SerializeField] private float _turnSmoothTime = 0.1f;
    [SerializeField] private Camera cam;
    private Vector3 camEuler;
    private Vector3 moveDir;
    private float gravity;
    private float gravityForce = -10f;
    private float jumpForce = 100f;
    private float jump;
    private PickDrop pickDrop;
    private bool isJump = false;

    void Update()
    {
        
        MoveAndRotate();
    }

    private void MoveAndRotate()
    {
        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");
        if (CheckPipeStatus() && Input.GetMouseButton(1)) //CheckPipeStatus() &&
        {
            controller.enabled = false;
        }
        else
        {
            controller.enabled = true;
        }
        transform.rotation = Quaternion.Euler(0f, cam.transform.eulerAngles.y, 0f);
        Vector3 direction = new Vector3(_horizontal, 0f, _vertical).normalized;
        float TargetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
        moveDir = Quaternion.Euler(0f, TargetAngle, 0f) * Vector3.forward;
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            jump = jumpForce * Time.deltaTime * 5;
            gravity = 0;
            isJump = true;
        }
        else if (!controller.isGrounded)
        {
            gravity = gravityForce * Time.deltaTime * 5;
            if (isJump)
            {
                jump *= 0.9f;
            }
        }
        else
        {
            gravity = 0;
            jump = 0;
            isJump = false;
        }
        moveDir.y += jump;
        if (direction.magnitude >= 0.1f && controller.enabled || !controller.isGrounded)//|| !controller.isGrounded
        {

            moveDir.y += gravity;
            controller.Move(moveDir * speed);
        }
        //else if (!controller.isGrounded)
        //{
        //    //moveDir.x = 0;
        //    //moveDir.z = 0;
        //    moveDir.y += gravity;
        //    controller.Move(moveDir * speed);
        //}
        //Debug.Log(transform.position.y);
    }


    private bool CheckPipeStatus()
    {
        pickDrop = GetComponentInChildren<PickDrop>();
        if (pickDrop != null)
        {
            //Debug.Log("a");
            //if (GetComponentInChildren<PickDrop>().isRotating)
            if (pickDrop.isPickUp)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void Start()
    {
        //pickDrop = GetComponentInChildren<PickDrop>();
    }


    //IEnumerator Jump()
    //{
    //    float inTime = 0.5f;
    //    for (float t = 0f; t <= 1; t += Time.deltaTime / inTime)
    //    {
    //        //transform.localEulerAngles = Vector3.Lerp(currentRotation, currentRotation + byAngles, t);
    //        //transform.localRotation = Quaternion.Lerp(fromAngle, toAngle, t);
    //        jumpForce = jump * t;
    //        yield return null;
    //    }
    //}
}
