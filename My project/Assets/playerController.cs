using System;
using UnityEngine;

public class playerController : MonoBehaviour
{

    private bool rotateRight = false;
    private bool rotateLeft = false;

    private float target_degrees = 0;
    private float r;

    public int rotateSpeed = 5; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && !rotateRight)
        {
            rotateRight = true;
            target_degrees = target_degrees + 90;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !rotateLeft)
        {
            rotateLeft = true;
            target_degrees = target_degrees - 90;
        }
        if (rotateRight)
        {
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target_degrees, ref r, 1f / rotateSpeed);
            
            transform.rotation = Quaternion.Euler(0, angle, 0);
            if (transform.rotation.eulerAngles.y > target_degrees - 0.1 && transform.rotation.eulerAngles.y < target_degrees + 0.1)
            {
                transform.rotation = Quaternion.Euler(0, target_degrees, 0);
                rotateRight = false;
            }
        }
        if (rotateLeft)
        {
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target_degrees, ref r, 1f / rotateSpeed);
            
            transform.rotation = Quaternion.Euler(0, angle, 0);
            if (transform.rotation.eulerAngles.y > target_degrees - 0.1 && transform.rotation.eulerAngles.y < target_degrees + 0.1)
            {
                transform.rotation = Quaternion.Euler(0, target_degrees, 0);
                rotateLeft = false;
            }
        }
        if (target_degrees >= 360) target_degrees -= 360;
        else if (target_degrees < 0) target_degrees += 360;
        Debug.Log(transform.eulerAngles.y);
    }
}
