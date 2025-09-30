using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    
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
        Debug.Log("Draggign");
        transform.position = Input.mousePosition;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}
