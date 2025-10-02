using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public string itemName;        //this might not be needed anymore
    public Sprite inventoryIcon;
    public bool isTransformable = false;
    
    //List of all the items used in the game through the inventory
    public enum ItemType
    {
        Flower,
        Photo,
        MatchBox,
        Pipe,
        Radio,
        LightBulb,
        Doll,
        Rock,
        PaintedLightBulb,
        Paste,
        CabinetKey
    }
    public ItemType itemType; 
    
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
            Debug.LogError("Inventory not found");
            return;
        }
        InventoryManager.Instance.AddItemToInventory(this.gameObject, inventoryIcon, itemName, isTransformable, itemType);
    }
}