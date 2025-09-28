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
        // Find an empty slot
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
}