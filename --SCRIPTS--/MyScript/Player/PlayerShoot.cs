using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

public class PlayerShoot : MonoBehaviour
{
    private Animator anim;
    public GameObject gun;

    public Transform bulletSpawnPoint;
    
    
    private bool isShooting = false;

    public GameObject fireEffect;
    public InventoryManager inventoryManager;
 
    private int range = 100;
    public GameObject light;
    float inputY;
    public GameObject mirino;

    public Transform orientation;

    private void Start()
    {
        anim = GetComponent<Animator>();


    }

    private void Update()
    {



        inputY = Input.GetAxis("Vertical");

        Debug.Log(inventoryManager.items.Count);
        //Debug.Log(inventoryManager.items.ToArray());
        if(inputY == 0 && HadTheGun())
        {
            mirino.SetActive(true);
        }
        else
        {
            mirino.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0) && HadTheGun() && inputY == 0 && isShooting)
        {
            anim.SetTrigger("Shoot");

            light.SetActive(true);
            Shoot();


        }
        else
        {
            Debug.Log("Equipaggia prima l'arma");
        }
       


        // fire anim with RayCast
        if (Input.GetMouseButtonUp(0))
        {
            light.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && !isShooting)
        {
            ChangeAnimatorState();
            isShooting = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && isShooting)
        {
            anim.SetLayerWeight(1, 0);

            anim.SetLayerWeight(0, 1);
            
            anim.SetLayerWeight(2, 0);
            gun.SetActive(false);
            isShooting = false;
        }
        if (Input.GetMouseButton(1) && HadTheGun())
        {
            light.SetActive(true);
        }
        if (Input.GetMouseButtonUp(1) && HadTheGun())
        {
            light.SetActive(false);
        }

    }




    private void ChangeAnimatorState()
    {
        bool hadGun = HadTheGun();
        if (hadGun)
        {

            anim.SetLayerWeight(0,0);
            anim.SetLayerWeight(1, 1);
            anim.SetLayerWeight(2, 0);

            gun.SetActive(true);
            // isShooting = true;


        }
        else
        {
            Debug.Log("Non hai armi");

        }


    }



    private bool HadTheGun()
    {

        int count = 0;
        if (inventoryManager.items != null)
        {
            foreach (Item item in inventoryManager.items)
            {
                if (item.type.ToString() == "Weapon")
                {
                    count++;
                }

            }

        }
        else
        {
            Debug.Log("Lista vuota");
            return false;
        }

        if (count == 0)
            return false;
        else return true;
    }




    void Shoot()
    {

        if (isShooting){ 

        RaycastHit hit;
        if (Physics.Raycast(orientation.transform.position, orientation.transform.forward, out hit, range)) {


            Instantiate(fireEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }



        /* GameObject spell = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
         spell.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * 40f;
         anim.SetTrigger("Shoot");
         Destroy(spell, 5f);
        */

    }
}



    }



