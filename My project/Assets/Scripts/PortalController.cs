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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    bool lastOneWas = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (lastOneWas)
            {
                OpenPortal("Flower");
            }
            else
            {
                OpenPortal("Lighthouse");
            }
            lastOneWas = !lastOneWas;
        }
    }


    public void ClosePortal()
    {
        spriteRenderer.sprite = originalSprite;
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
    }
    private void OnCollisionEnter(Collision other)
    {
        OpenPortal("Flower");
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        OpenPortal("Flower");
    }
}
