using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public string itemName;
    public Sprite inventoryIcon;
    
    private void Start()
    {
        if (GetComponent<Collider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }
    }
    
    void OnMouseDown()
    {
        Pickup();
    }
    
    void Pickup()
    {
        if (InventoryManager.Instance == null)
        {
            Debug.LogError("InventoryManager not found!");
            return;
        }
        
        InventoryManager.Instance.AddItemToInventory(this.gameObject, inventoryIcon, itemName);
    }
}