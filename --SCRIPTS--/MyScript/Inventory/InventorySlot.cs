using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem
{



    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        public Image image;


        public void Select()
        {

            image.color = Color.red;
        }

        public void Deselect()
        {

            image.color = Color.white;
        }



        public void OnDrop(PointerEventData eventData)
        {
            if (transform.childCount == 0) //se lo slot non è occupato da un altro item
            {
                InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
                inventoryItem.parentAfterDrag = transform;
            }
        }



    }

}