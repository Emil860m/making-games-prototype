using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Transform inventorySlotsParent;
    public GameObject inventoryItemPrefab;
    private List<GameObject> inventoryItems = new List<GameObject>();
    
    private Dictionary<(WorldItem.ItemType, WorldItem.ItemType), WorldItem.ItemType> combinations = new Dictionary<(WorldItem.ItemType, WorldItem.ItemType), WorldItem.ItemType>()
    {
        {(WorldItem.ItemType.LightBulb, WorldItem.ItemType.Paste), WorldItem.ItemType.PaintedLightBulb},
        {(WorldItem.ItemType.Doll, WorldItem.ItemType.Rock), WorldItem.ItemType.CabinetKey}
    };

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

    public void AddItemToInventory(GameObject worldItem, Sprite itemIcon, string itemName, bool isTransformable, WorldItem.ItemType itemType, Transform slot = null)
    {
        //Check if there is a specific slot provided to add the item if no search for empty slots
        if (slot == null)
        {
            slot = FindEmptySlot();
            if (slot == null)
            {
                Debug.LogWarning("No empty slots in inventory");
                return;
            }
        }

        GameObject newInventoryItem = Instantiate(inventoryItemPrefab, slot);
        InventoryItem itemComponent = newInventoryItem.GetComponent<InventoryItem>();
        
        if (itemComponent != null)
        {
            itemComponent.Initialize(itemIcon, worldItem, itemName, isTransformable, itemType);
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
    
    public void CombineItems(InventoryItem item1, InventoryItem item2, Transform slot)
    {
        if ((combinations.TryGetValue((item1.itemType, item2.itemType), out WorldItem.ItemType result)) || (combinations.TryGetValue((item2.itemType, item1.itemType), out result)))
        {
            PerformCombination(item1, item2, result, slot);
        }
        else
        {
            Debug.Log("Cannot combine these items");
        }
    }
    
    private void PerformCombination(InventoryItem item1, InventoryItem item2, WorldItem.ItemType resultType, Transform slot)
    {
        GameObject resultWorldItem = FindWorldItem(resultType);
        WorldItem worldItemComponent = resultWorldItem.GetComponent<WorldItem>();
            
        if (resultWorldItem != null)
        {
            Destroy(item1.gameObject);
            Destroy(item2.gameObject);
            AddItemToInventory(resultWorldItem, worldItemComponent.inventoryIcon, worldItemComponent.itemName, worldItemComponent.isTransformable, worldItemComponent.itemType, slot);
            Debug.Log("Items combined");
        }
        else
        {
            Debug.LogError("Error in combining items");
        }
    }
    
    private GameObject FindWorldItem(WorldItem.ItemType itemType)
    {
        WorldItem[] worldItems = FindObjectsByType<WorldItem>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (WorldItem worldItem in worldItems)
        {
            if (worldItem.itemType == itemType)
                return worldItem.gameObject;
        }
        return null;
    }
}