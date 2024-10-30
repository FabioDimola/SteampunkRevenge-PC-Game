using InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{

    public InventoryManager inventoryManager;
    
    public Transform dropPoint;
    public InventoryItem inventoryItem;



    public void DroppItem()
    {
        
        inventoryManager.Remove(inventoryItem.item);
    }




}
