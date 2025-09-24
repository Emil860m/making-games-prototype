using System;
using UnityEngine;

public class playerController : MonoBehaviour
{

    private bool rotateRight = false;
    private bool rotateLeft = false;
    private bool moveForward = false;

    private float target_degrees = 0;
    private float r;

    public ViewBobController viewBobSystem;

    

    public int speed = 5; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && !rotateRight && !rotateLeft && !moveForward)
        {
            rotateRight = true;
            target_degrees = target_degrees + 90;
            viewBobSystem.SetViewBob(true);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !rotateRight && !rotateLeft && !moveForward)
        {
            rotateLeft = true;
            target_degrees = target_degrees - 90;
            viewBobSystem.SetViewBob(true);
        }
        // If we do extending rooms: 
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && !rotateRight && !rotateLeft && !moveForward)
        {
            moveForward = true;
            viewBobSystem.SetViewBob(true);
        }
        if (rotateRight)
        {
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target_degrees, ref r, 1f / speed);
            
            transform.rotation = Quaternion.Euler(0, angle, 0);
            if (transform.rotation.eulerAngles.y > target_degrees - 0.1 && transform.rotation.eulerAngles.y < target_degrees + 0.1)
            {
                transform.rotation = Quaternion.Euler(0, target_degrees, 0);
                rotateRight = false;
                viewBobSystem.SetViewBob(false);
            }
        }
        if (rotateLeft)
        {
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target_degrees, ref r, 1f / speed);
            
            transform.rotation = Quaternion.Euler(0, angle, 0);
            if (transform.rotation.eulerAngles.y > target_degrees - 0.1 && transform.rotation.eulerAngles.y < target_degrees + 0.1)
            {
                transform.rotation = Quaternion.Euler(0, target_degrees, 0);
                rotateLeft = false;
                viewBobSystem.SetViewBob(false);
            }
        }
        if (target_degrees >= 360) target_degrees -= 360;
        else if (target_degrees < 0) target_degrees += 360;
        if (moveForward)
        {
            transform.position = transform.position + (transform.forward * Time.deltaTime * speed);
            if (transform.position.z > 0 && target_degrees == 0)
            {
                transform.position = Vector3.zero;
                moveForward = false;
                viewBobSystem.SetViewBob(false);
            }
        }
    }
}
