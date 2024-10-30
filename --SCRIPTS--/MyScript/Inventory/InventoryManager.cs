using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace InventorySystem
{

    public class InventoryManager : MonoBehaviour
    {

        //public InventorySlot[] inventorySlots; //array che contiene tutti gli slot presenti


        //Differenzio slots in base al tipo di item
        public InventorySlot[] potionSlot;
        public InventorySlot[] weaponSlot;
        public InventorySlot[] handWeaponSlot;
        public InventorySlot[] researchSlot;
        public InventorySlot[] utilitySlot;
        public GameObject inventoryItemPrefab;
        private int maxValue = 100;

        public PlayerHealth playerHealth;


        public List<Item> items; 

       



        int selectedSlot = -1;

        private void Start()
        {
            //ChangeSelectedSlot(0);
            items = new List<Item>();
        }


        private void Update()
        {
           
        }

        /* void ChangeSelectedSlot(int newValue)
         {
             if(selectedSlot >= 0)
             {
                 inventorySlots[selectedSlot].Deselect();
             }

             inventorySlots[newValue].Select();
             selectedSlot = newValue;
         }*/

        /*   private void Update()
           {   //cambio casella selezionata con tasti numerici (1-6) tastiera
               if (Input.GetKeyDown(KeyCode.Alpha1))
               {
                   ChangeSelectedSlot(0);
               }else if(Input.GetKeyDown(KeyCode.Alpha2)) { 
                   ChangeSelectedSlot(1); 
               }
               else if (Input.GetKeyDown(KeyCode.Alpha3))
               {
                   ChangeSelectedSlot(2);
               }
               else if (Input.GetKeyDown(KeyCode.Alpha4))
               {
                   ChangeSelectedSlot(3);
               }
               else if (Input.GetKeyDown(KeyCode.Alpha5))
               {
                   ChangeSelectedSlot(4);
               }
               else if (Input.GetKeyDown(KeyCode.Alpha6))
               {
                   ChangeSelectedSlot(5);
               }
               else if (Input.GetKeyDown(KeyCode.Alpha7))
               {
                   ChangeSelectedSlot(6);
               }

           }

           */







        public bool AddItems(Item item, InventorySlot[] slots)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                InventorySlot slot = slots[i]; //tempVar
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxValue)
                {
                    //l'item è già presente nell'inventario quindi aumento quantità e non aggiungo nuovo elemento
                    itemInSlot.count++;
                    itemInSlot.RefreshCountText();
                    return true;
                }

            }

            //trova slot vuoto
            for (int i = 0; i < slots.Length; i++)
            {
                InventorySlot slot = slots[i]; //tempVar
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                items.Add(item);
                if (itemInSlot == null)
                {
                    //slot vuoto e non c'è ancora nell'inventario
                    SpawnItem(item, slot);
                    return true;

                }
            }

            return false;
        }


     




        public bool RemoveItems(Item item, InventorySlot[] slots)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                InventorySlot slot = slots[i]; //tempVar
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count > 1)
                {
                    //test per utilizzo cura
                    if (itemInSlot.item.type.ToString() == "Health" && PlayerHealth.instance.currentHealth < 100)
                    {
                        PlayerHealth.instance.currentHealth += 10;
                        Health.instance.UpdateHealthBar(100, PlayerHealth.instance.currentHealth);

                    }


                    //l'item è già presente nell'inventario quindi decremento quantità 
                    itemInSlot.count--;
                    itemInSlot.RefreshCountText();
                    return true;
                }
                else if (itemInSlot != null && itemInSlot.count == 1) // Se è rimasta l'ultima unità cancello lo slot
                {
                    //test per utilizzo cura
                    if (itemInSlot.item.type.ToString() == "Health" && PlayerHealth.instance.currentHealth < 100)
                    {
                        PlayerHealth.instance.currentHealth += 10;
                        Health.instance.UpdateHealthBar(100, PlayerHealth.instance.currentHealth);
                    }
                    Destroy(itemInSlot.gameObject);
                }

            }



            return false;
        }





      





        public void Remove(Item item)
        {
            string itemType = item.type.ToString();



            switch (itemType)
            {

                case "Weapon":
                    RemoveItems(item, weaponSlot);
                    break;
                case "MediKit":
                    RemoveItems(item, potionSlot);
                    break;
                case "Utility":
                    RemoveItems(item, utilitySlot);
                    break;
                case "Health":
                    RemoveItems(item, potionSlot);
                    break;
                case "Research":
                    RemoveItems(item, researchSlot);
                    break;
               case "HandWeapon":
                    RemoveItems(item, handWeaponSlot);
                    break;


            }

        }






        public void Add(Item item)
        {
            string itemType = item.type.ToString();



            switch (itemType)
            {

                case "Weapon":
                    AddItems(item, weaponSlot);
                    break;

                case "Utility":
                    AddItems(item, utilitySlot);
                    break;
                case "Health":
                    AddItems(item, potionSlot);
                    break;
                case "MediKit":
                    AddItems(item, potionSlot);
                    break;
                case "Research":
                    AddItems(item, researchSlot);
                    break;
                case "Potion":
                    AddItems(item, potionSlot);
                    break;
                     case "HandWeapon":
                    AddItems(item, handWeaponSlot);
                    break;
            }

        }


        void SpawnItem(Item item, InventorySlot slot)
        {
            GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
            InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
            inventoryItem.InitialiseItem(item);


        }




        public void DisableSprite()
        {
            potionSlot[0].image.gameObject.SetActive(false);
        }

        /* public bool AddItem(Item item) //ricerca tra tutti gli slot qullo non occupato e inserisce item
        {

            if (item.type.ToString() == "Weapon")
            {
                //controllo se è già presente uno slot con stesso item con qty < max
                for (int i = 0; i < weaponSlot.Length; i++)
                {


                    InventorySlot slot = weaponSlot[i]; //temp var 
                    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>(); //temp var per gli slot analizzati
                    if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxValue)
                    {
                        itemInSlot.count++;
                        itemInSlot.RefreshCountText();
                        return true;
                    }
                }




                //trova slot vuoto
                for (int i = 0; i < weaponSlot.Length; i++)
                {
                    InventorySlot slot = weaponSlot[i]; //temp var 
                    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>(); //temp var per gli slot analizzati
                    if (itemInSlot == null)
                    {
                        //se non c'è nessun item nello slot
                        SpawnItem(item, slot); //aggiunge l'item nello slot
                        return true;
                    }
                }

            }else if(item.type.ToString() == "Health")
            {
                //controllo se è già presente uno slot con stesso item con qty < max
                for (int i = 0; i < potionSlot.Length; i++)
                {


                    InventorySlot slot = potionSlot[i]; //temp var 
                    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>(); //temp var per gli slot analizzati
                    if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxValue)
                    {
                        itemInSlot.count++;
                        itemInSlot.RefreshCountText();
                        return true;
                    }
                }




                //trova slot vuoto
                for (int i = 0; i < potionSlot.Length; i++)
                {
                    InventorySlot slot = potionSlot[i]; //temp var 
                    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>(); //temp var per gli slot analizzati
                    if (itemInSlot == null)
                    {
                        //se non c'è nessun item nello slot
                        SpawnItem(item, slot); //aggiunge l'item nello slot
                        return true;
                    }
                }

            }else if(item.type.ToString() == "Research")
            {
                //controllo se è già presente uno slot con stesso item con qty < max
                for (int i = 0; i < researchSlot.Length; i++)
                {


                    InventorySlot slot =researchSlot[i]; //temp var 
                    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>(); //temp var per gli slot analizzati
                    if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxValue)
                    {
                        itemInSlot.count++;
                        itemInSlot.RefreshCountText();
                        return true;
                    }
                }




                //trova slot vuoto
                for (int i = 0; i < researchSlot.Length; i++)
                {
                    InventorySlot slot = researchSlot[i]; //temp var 
                    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>(); //temp var per gli slot analizzati
                    if (itemInSlot == null)
                    {
                        //se non c'è nessun item nello slot
                        SpawnItem(item, slot); //aggiunge l'item nello slot
                        return true;
                    }
                }

            }
            else if(item.type.ToString() == "Utility")
            {
                //controllo se è già presente uno slot con stesso item con qty < max
                for (int i = 0; i < utilitySlot.Length; i++)
                {


                    InventorySlot slot = utilitySlot[i]; //temp var 
                    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>(); //temp var per gli slot analizzati
                    if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxValue)
                    {
                        itemInSlot.count++;
                        itemInSlot.RefreshCountText();
                        return true;
                    }
                }




                //trova slot vuoto
                for (int i = 0; i < utilitySlot.Length; i++)
                {
                    InventorySlot slot = utilitySlot[i]; //temp var 
                    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>(); //temp var per gli slot analizzati
                    if (itemInSlot == null)
                    {
                        //se non c'è nessun item nello slot
                        SpawnItem(item, slot); //aggiunge l'item nello slot
                        return true;
                    }
                }
            }

            return false;
        }
        */




        /* public Item GetSelectedItem(bool use)
         {
             InventorySlot slot = inventorySlots[selectedSlot];
             InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
             if (itemInSlot != null)
             {
                 Item item= itemInSlot.item;
                 if (use)
                 {
                     itemInSlot.count--;
                     if(itemInSlot.count <= 0)
                     {
                         Destroy(itemInSlot.gameObject);
                     }
                     else
                     {
                         itemInSlot.RefreshCountText();
                     }

                 }
             }
             return null;
         }
        */
    }
}