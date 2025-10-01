using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
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
        //Debug.Log("Draggign");
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
        Debug.Log("End drag");
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        if (parentAfterDrag.name == "PortalDoor")
        {
            transform.SetParent(transform.parent.parent);
            transform.position = parentAfterDrag.position + new Vector3(2f, 0, 0.39f);
            PortalController portalCon = worldInteractionObject.GetComponent<PortalController>();
            portalCon.OpenPortal("Flower");

        }
    }


}
