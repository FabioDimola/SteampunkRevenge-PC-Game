
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem
{
    public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Item item;

        public TMP_Text countText;

        [Header("Info")]
       [HideInInspector] public TMP_Text itemName;
        private TMP_Text itemDescription;
        private GameObject infoItemPanel;
        private Image infoImage;


        [Header("Button")]
        public GameObject removePotion;
        public GameObject removeEndurance;
        public GameObject removeGun;
        public GameObject removeFood;

        bool firstTime = true;



        public Image image;
        [HideInInspector] public Transform parentAfterDrag;
        [HideInInspector] public int count = 1; //item start with 1 qty

        [SerializeField] private InventoryManager inventoryManager;


        public GameObject player;

       
        



        private void Start()
        {
          
           
            infoItemPanel = GameObject.FindGameObjectWithTag("InfoPanel");
            itemName = infoItemPanel.GetComponentsInChildren<TMP_Text>().Last();
            itemDescription = infoItemPanel.GetComponentInChildren<TMP_Text>();
            infoImage = infoItemPanel.GetComponentsInChildren<Image>().Last();
            Debug.Log("C'è scritto :" + itemName.text.ToString());
        }


      
        private void Update()
        {
           
        }
        public  void SetInfo()
        {
            infoItemPanel.SetActive(true);
            itemName.text = item.name;
            itemDescription.text = item.description;
            infoImage.sprite = item.icon;
           // InstantiateFirstButton();
           // ActiveButton();

            Debug.Log(item.name.ToString());
        }


        public void InstantiateFirstButton()
        {
            if (firstTime)
            {
                switch (itemName.text.ToString())
                {

                    case "Health":
                        Instantiate(removePotion, removePotion.transform);
                        firstTime = false;
                        break;

                    case "Gun Classic":
                        Instantiate(removeGun, removeGun.transform);
                        break;
                    case "Beans":
                        Instantiate(removeFood, removeFood.transform);
                        break;
                    case "Endurance":
                        Instantiate(removeEndurance, removeEndurance.transform);
                        break;


                }




            }

           
        }





        public void ActiveButton()
        {
            switch (itemName.text.ToString())
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

        public void OnBeginDrag(PointerEventData eventData) //inizio del drag
        {
            image.raycastTarget = false;
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
        }

        public void OnDrag(PointerEventData eventData)//quando l'oggetto è stato deaggato
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData) // fine del drag
        {
            image.raycastTarget = true;
            transform.SetParent(parentAfterDrag);
        }


        public void DeInizialized(Item _item)
        {
            Destroy(_item);
            Destroy(_item.icon);
        }

        public void InitialiseItem(Item newItem)
        {
            item = newItem;
            image.sprite = newItem.icon;
            RefreshCountText();
        }

        public void RefreshCountText()
        {
            bool textActive = count > 1;
            countText.text = count.ToString();
            countText.gameObject.SetActive(textActive);
        }


       


     public void Remove()
        {
            count--;
            countText.text = count.ToString();
        }


       
    }

}