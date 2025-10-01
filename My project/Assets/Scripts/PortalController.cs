using UnityEngine;
using UnityEngine.EventSystems;

public class PortalController : MonoBehaviour
{
    private BoxCollider boxCollider;
    private SpriteRenderer spriteRenderer;

    public Sprite sprite;
    private Sprite originalSprite;
    public GameObject flowerRoom;
    public GameObject LightHouseRoom;
    public DoorController PortalDoor;
    private bool portalOpen;
    private bool doorOpen;
    private InWorldSlot item;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        item = GetComponent<InWorldSlot>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    void Update()
    {
        if (item.IsOccupied())
        {
            OpenPortal(item.GetItemName());
        }
        else
        {
            PortalDoor.Close();
        }
    }


    public void ClosePortal()
    {
        spriteRenderer.sprite = originalSprite;
        boxCollider.enabled = true;
    }

    public void OpenPortal(string room)
    {
        if (room == "Flower")
        {
            LightHouseRoom.transform.localPosition = new Vector3(-16.1f, 0, -16.1f);
            flowerRoom.transform.localPosition = new Vector3(0, 0, -16.1f);
        }
        else if (room == "Lighthouse")
        {
            flowerRoom.transform.localPosition = new Vector3(-16.1f, 0, -16.1f);
            LightHouseRoom.transform.localPosition = new Vector3(0, 0, -16.1f);
        }
        else
        {
            Debug.Log("Error: Wrong room: " + room);
            return;
        }
        spriteRenderer.sprite = sprite;
        boxCollider.enabled = false;
        PortalDoor.Open();
    }
}
