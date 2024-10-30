using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InventorySystem
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Item", fileName = "Item")]
    public class Item : ScriptableObject
    {
        public string itemName;
        [TextArea(5, 5)]
        public string description;
        public int stackSize;
        public ItemType type;
        public Sprite icon;

        public GameObject prefab;
    }


    public enum ItemType
    {
        Health,
        Potion,
        Weapon,
        HandWeapon,
        Research,
        Utility,
        MediKit
    }

}