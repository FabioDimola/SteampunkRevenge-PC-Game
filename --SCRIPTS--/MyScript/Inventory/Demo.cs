using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace InventorySystem
{
    public class Demo : MonoBehaviour
    {
        public InventoryManager inventoryManager;
        public Item[] itemsToPickUp;
        public Transform dropPoint;

        [Header("Button")]
        public GameObject removePotion;
        public GameObject removeEndurance;
        public GameObject removeGun;
        public GameObject removeFood;


        public InventoryItem itemInSlot;
        public void PickUpItem(int id)
        {
            inventoryManager.Add(itemsToPickUp[id]);
            /* if (result)
              {
                  Debug.Log("Item Added");
              }
              else
              {
                  Debug.Log("Inventory Full");
              }*/
        }


        public void DroppItem(int id)
        {
            inventoryManager.Remove(itemsToPickUp[id]);
        }


        public void DroppItemInGame(int id)
        {
            
            Instantiate(itemsToPickUp[id].prefab, dropPoint.position, Quaternion.identity);
            inventoryManager.Remove(itemsToPickUp[id]);
            Debug.Log("Rimosso");
            
        }



      




        public void ActiveButton()
        {
            switch (itemInSlot.itemName.text.ToString())
            {
                case "Health":
                    Debug.Log("Health");
                    removeEndurance.SetActive(false);
                    removeFood.SetActive(false);
                    removeGun.SetActive(false);
                    removePotion.SetActive(true);
                    break;

                case "Gun Classic":
                    removeEndurance.SetActive(false);
                    removeFood.SetActive(false);
                    removeGun.SetActive(true);
                    removePotion.SetActive(false);
                    break;
                case "Beans":
                    removeEndurance.SetActive(false);
                    removeFood.SetActive(true);
                    removeGun.SetActive(false);
                    removePotion.SetActive(false);
                    break;
                case "Endurance":
                    removeEndurance.SetActive(true);
                    removeFood.SetActive(false);
                    removeGun.SetActive(false);
                    removePotion.SetActive(false);
                    break;
            }

        }

        /*  public void GetSelectedItem()
           {
               Item recivedItems = inventoryManager.GetSelectedItem(false);
               if (recivedItems != null)
               {
                   Debug.Log("Recived item" + recivedItems);
               }
               else
               {
                   Debug.Log("Not recived");
               }
           }


           public void UseSelectedItem()
           {
               Item recivedItems = inventoryManager.GetSelectedItem(true);
               if (recivedItems != null)
               {
                   Debug.Log("Use Items" + recivedItems);
               }
               else
               {
                   Debug.Log("Not used");
               }
           }
        */

    }

}