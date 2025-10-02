using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public GameObject worldItem;
    [HideInInspector] public string itemName;
    [HideInInspector] public bool isTransformable;
    [HideInInspector] public WorldItem.ItemType itemType;
    
    public void Initialize(Sprite sprite, GameObject originalWorldItem, string name, bool transformable, WorldItem.ItemType type)
    {
        image.sprite = sprite;
        worldItem = originalWorldItem;
        itemName = name;
        image.raycastTarget = true;
        isTransformable = transformable;
        itemType = type;
    }
    public WorldDrag wd;
    public GameObject worldInteractionObject;
    public RectTransform canvas;


    public void ReturnToUI()
    {
        transform.SetParent(canvas);
        //transform.localScale = Vector3.one;
        transform.position = Input.mousePosition; // or anchored position if needed
        image.raycastTarget = true;
        PortalController portalCon = worldInteractionObject.GetComponent<PortalController>();
        portalCon.ClosePortal();
        // Remove world collider if it exists
        //Collider col = GetComponent<Collider>();
        //if (col) Destroy(col);
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");
        transform.position = Input.mousePosition;
        if (wd != null)
        {
            Transform tf = wd.DragToWorld("PortalDoor");
            if (tf != null)
            {
                parentAfterDrag = tf;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        
        if (TryFindInWorldSlot(out InWorldSlot worldSlot))
        {
            InventoryManager.Instance.AddItemToWorld(this, worldSlot);
        }
        else
        {
            ReturnToInventory();
        }
    }

    private bool TryFindInWorldSlot(out InWorldSlot worldSlot)
    {
        worldSlot = null;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //check if i need camera position
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform);
        }

            //RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 100f);
        //Debug.Log(hit.transform);
        
        if (hit.collider != null)
        {
            worldSlot = hit.collider.GetComponent<InWorldSlot>();
            if (worldSlot != null && !worldSlot.IsOccupied())
            {
                return true;
            }
        }
        
        return false;
    }
    
    private void ReturnToInventory()
    {
        transform.SetParent(parentAfterDrag);
        transform.localPosition = Vector3.zero;
    }


}
