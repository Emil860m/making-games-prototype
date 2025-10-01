using UnityEngine;

public class InWorldSlot : MonoBehaviour 
{
    public bool IsOccupied()
    {
        if (transform.childCount == 0)
            return false;
    
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy)
                return true;
        }
        
        //Include something about make each placement zone specific for an item.
        
        return false;
    }

    public string GetItemName()
    {
        if (IsOccupied())
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeInHierarchy)
                    return child.gameObject.GetComponent<WorldItem>().itemName;
            }
        }
        return "";
    }
}