using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InventorySystem
{
    public class PlayerPickUp : MonoBehaviour
    {
        [SerializeField] private InventoryManager inventoryManager;
        private int currentPaper = 0;
        public GameObject WinPanel;

        public AudioSource reearchSX;
        public AudioSource pickUpItem;
        private void Update()
        {
            GoToNextLevel();
        }

        private void GoToNextLevel()
        {
            if(currentPaper == 10)
            {
                WinPanel.SetActive(true);
                Time.timeScale = 0;
               if( SceneManager.GetActiveScene().buildIndex == 1)
                StartCoroutine(NextLevel());
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out ItemPickUp pickUp))
            {
                if(pickUp.item.type == ItemType.Research)
                {
                    currentPaper+=1;
                    reearchSX.Play();

                }
                else
                {
                    pickUpItem.Play();
                }
                PaperManager.Instance.UpdatePaperText(currentPaper, 10);
                inventoryManager.Add(pickUp.item);
                Destroy(other.gameObject);
            }
        }


        IEnumerator NextLevel()
        {
            int i = 1;
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(i);
            i++;
        }




    }

    
    


    }

