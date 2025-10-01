using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class playerController : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public Camera m_Camera;

    private bool rotateRight = false;
    private bool rotateLeft = false;
    private bool moveForward = false;

    private float target_degrees = 0;
    private float r;

    public ViewBobController viewBobSystem;
    private Vector3 direction;
    public GameObject levelsParent;
    private List<GameObject> _allPositions = new List<GameObject>();


    public int speed = 5;

    private GameObject currentGO;

    // Start only called once when the object is loaded.
    void Start()
    {
        for (int i = 0; i < levelsParent.transform.childCount; i++)
        {
            var level = levelsParent.transform.GetChild(i).gameObject;
            _allPositions.Add(level);
        }

        // Initialize currentPosition to the closest position at start
        currentGO = GetClosestPosition(transform.position);
        _allPositions.Remove(currentGO);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Input.mousePosition;
            var worldRay = m_Camera.ScreenPointToRay(pos);

            RaycastHit hit;
            if (Physics.Raycast(worldRay, out hit))
            {
                if (hit.transform.GetComponent<InventoryItem>() != null)
                    hit.transform.GetComponent<InventoryItem>().ReturnToUI();
                if (hit.transform.name == "PortalDoor")
                {
                    hit.transform.parent.gameObject.GetComponent<DoorController>().UseDoor();
                }
            }
        }
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
            direction = transform.forward;
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
            transform.position = transform.position + (direction * Time.deltaTime * speed);

            this.MoveToPosition();
        }
    }

    public void MoveToPosition()
    {
        foreach (var posObj in _allPositions)
        {
            Vector3 pos = posObj.transform.position;

            float playerX = Mathf.Round(transform.position.x);
            float playerZ = Mathf.Round(transform.position.z);
            float targetX = Mathf.Round(pos.x);
            float targetZ = Mathf.Round(pos.z);

            if (Mathf.Abs(playerX - targetX) <= 0.5f && Mathf.Abs(playerZ - targetZ) <= 0.5f)
            {
                // Snap to new position
                transform.position = Vector3.MoveTowards(transform.position, pos, 0.5f);
                moveForward = false;
                viewBobSystem.SetViewBob(false);

                // Re-add previous position to the list
                if (currentGO != null)
                    _allPositions.Add(currentGO);


                // Update currentPosition and remove it from the list
                currentGO = posObj;
                _allPositions.Remove(currentGO);
                break;
            }
        }
    }

    // Helper to find the closest position at start
    private GameObject GetClosestPosition(Vector3 position)
    {
        GameObject closest = null;
        float minDist = float.MaxValue;
        foreach (var posObj in _allPositions)
        {
            float dist = Vector3.Distance(position, posObj.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = posObj;
            }
        }
        return closest;
    }


    void OnCollisionEnter(Collision collision)
    {
        _allPositions.Add(currentGO);
        currentGO = null;
        direction *= -1;
        viewBobSystem.amount *= 10;
    }
}
