using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject menuImage;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            inventory.SetActive(true);
            menuImage.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
        
    }



    public void CloseMenu()
    {
        inventory.SetActive(false);
        menuImage.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
