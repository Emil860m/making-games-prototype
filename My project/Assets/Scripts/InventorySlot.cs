using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        InventoryItem draggableItem = dropped.GetComponent<InventoryItem>();
        if (draggableItem == null) return;

        Debug.Log("Dropped " + draggableItem.itemName + " into slot");
                
        if (transform.childCount == 0)
        {
            draggableItem.parentAfterDrag = transform;
        }
    }
}
