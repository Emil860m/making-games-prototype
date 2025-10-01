using UnityEngine;

public class DoorController : MonoBehaviour
{


    private bool opening = false;
    private bool closing = false;

    private int speed = 5;

    public float target_degrees = -90;

    private float r;

    private bool is_open = false;

    public void Open()
    {
        opening = true;
    }

    public void Close()
    {
        closing = true;
    }

    void Start()
    {
        while (target_degrees < 0)
        {
            target_degrees += 360;
        }
        while (target_degrees >= 360)
        {
            target_degrees -= 360;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (opening)
        {
            //transform.RotateAround(rotateAroundPosition.transform.position, Vector3.up, -speed * Time.deltaTime);
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target_degrees, ref r, 1f / speed);

            transform.rotation = Quaternion.Euler(0, angle, 0);
            if (transform.rotation.eulerAngles.y > target_degrees - 0.1 && transform.rotation.eulerAngles.y < target_degrees + 0.1)
            {
                transform.rotation = Quaternion.Euler(0, target_degrees, 0);
                opening = false;
            }
        }
        else if (closing)
        {
            //transform.RotateAround(pos, Vector3.up, speed * Time.deltaTime);
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 0, ref r, 1f / speed);

            transform.rotation = Quaternion.Euler(0, angle, 0);
            if (transform.rotation.eulerAngles.y > 0 - 0.1 && transform.rotation.eulerAngles.y < 0 + 0.1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                closing = false;
            }
        }
    }

    public void UseDoor()
    {
        if (!opening && !closing)
        {
            if (is_open)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }
}
