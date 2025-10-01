using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public GameObject worldItem;
    [HideInInspector] public string itemName;
    
    public void Initialize(Sprite sprite, GameObject originalWorldItem, string name)
    {
        image.sprite = sprite;
        worldItem = originalWorldItem;
        itemName = name;
        image.raycastTarget = true;
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
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 100f);
        
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
