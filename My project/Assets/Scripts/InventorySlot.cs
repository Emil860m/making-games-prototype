using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;
        
        InventoryItem existingItem = GetComponentInChildren<InventoryItem>();
        InventoryItem draggableItem = dropped.GetComponent<InventoryItem>();
        if (draggableItem == null) return;
        
        if (transform.childCount == 0)
        {
            draggableItem.parentAfterDrag = transform;
            draggableItem.transform.SetParent(transform);
            draggableItem.transform.localPosition = Vector3.zero;
            
            Debug.Log("Dropped " + draggableItem.itemName + " into inventory slot");
        }
        else if (draggableItem.isTransformable && existingItem.isTransformable)
        {
            InventoryManager.Instance.CombineItems(draggableItem, existingItem, transform);
        }
    }
}
