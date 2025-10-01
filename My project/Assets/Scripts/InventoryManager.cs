using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Transform inventorySlotsParent;
    public GameObject inventoryItemPrefab;
    private List<GameObject> inventoryItems = new List<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItemToInventory(GameObject worldItem, Sprite itemIcon, string itemName)
    {
        Transform emptySlot = FindEmptySlot();
        if (emptySlot == null)
        {
            Debug.LogWarning("No empty slots in inventory");
            return;
        }

        GameObject newInventoryItem = Instantiate(inventoryItemPrefab, emptySlot);
        InventoryItem itemComponent = newInventoryItem.GetComponent<InventoryItem>();
        
        if (itemComponent != null)
        {
            itemComponent.Initialize(itemIcon, worldItem, itemName);
            inventoryItems.Add(newInventoryItem);
            Debug.Log("Added " + itemName + " to inventory");
        }

        worldItem.SetActive(false);
    }
    
    public void AddItemToWorld(InventoryItem item, InWorldSlot worldSlot)
    {
        if (item.worldItem != null)
        {
            item.worldItem.SetActive(true);
            item.worldItem.transform.SetParent(worldSlot.transform);
            
            item.worldItem.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
            item.worldItem.transform.localPosition = new Vector3(0, -0.1f, -0.1f);
            item.worldItem.GetComponent<SpriteRenderer>().enabled = true;
            item.worldItem.transform.rotation = Quaternion.Euler(0,0,0);

            Debug.Log("Item in World");
        }
        
        RemoveItemFromInventory(item.gameObject);
        Destroy(item.gameObject);
    }

    private Transform FindEmptySlot()
    {
        foreach (Transform slot in inventorySlotsParent)
        {
            if (slot.childCount == 0)
            {
                return slot;
            }
        }
        return null;
    }
    
    private void RemoveItemFromInventory(GameObject inventoryItem)
    {
        if (inventoryItems.Contains(inventoryItem))
        {
            inventoryItems.Remove(inventoryItem);
        }
    }
}