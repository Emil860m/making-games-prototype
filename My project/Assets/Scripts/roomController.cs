using NUnit.Framework.Constraints;
using UnityEngine;

public class roomController : MonoBehaviour
{
    public GameObject wall;
    public GameObject player;

    private bool moveWall = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveWall = true;
        }
        if (moveWall)
        {
            wall.transform.position = wall.transform.position + (new Vector3(0, 0, 2) * Time.deltaTime);
        }
    }
}
