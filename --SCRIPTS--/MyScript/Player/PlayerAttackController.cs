using InventorySystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private Animator animator;

    [Header("Attack Input")]
    public float coolDown = 2f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    private float lastClickedTime = 0;
    private float maxComboDelay = 2f;




    [Header("Sword")]
    [SerializeField] private GameObject backSword;
    [SerializeField] private GameObject handSword;
    bool hadTheSword = false;


    [Header("Gun")]
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject fireVFX;
    [SerializeField] private GameObject mirino;
    private bool isShooting = false;
    private int range = 100;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform spawnPoint;
    


    [Header("Inventory")]
    [SerializeField] private InventoryManager inventoryManager;

    private bool swordState = false;
    private bool gunState = false;
    private float inputY, inputX;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;
        if(noOfClicks == 1)
        {
            animator.SetBool("Hit1", true);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if(noOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(2).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(2).IsName("Stable Sword Inward Slash"))
        {
            animator.SetBool("Hit1", false);
            animator.SetBool("Hit2", true);

        }
        if (noOfClicks >= 3 && animator.GetCurrentAnimatorStateInfo(2).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(2).IsName("Stable Sword Outward Slash"))
        {
            animator.SetBool("Hit2", false);
            animator.SetBool("Hit3", true);

        }
    }




    private void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        //sword state
        if (Input.GetKeyDown(KeyCode.Alpha1) )
        {
            animator.SetLayerWeight(2, 1);
            hadTheSword = true;
            animator.SetTrigger("Draw Sword");
            Debug.Log(hadTheSword);
   
            StartCoroutine("ChangeSword");
        }
        if(Input.GetKeyDown(KeyCode.Escape) )
        {
            animator.SetTrigger("Sheath Sword");

            StartCoroutine("SheathSword");
            hadTheSword = false;
            Debug.Log(hadTheSword);
        }


        if(animator.GetCurrentAnimatorStateInfo(2).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(2).IsName("Stable Sword Inward Slash"))
        {
            animator.SetBool("Hit1", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(2).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(2).IsName("Stable Sword Outward Slash"))
        {
            animator.SetBool("Hit2", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(2).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(2).IsName("Sword And Shield Attack"))
        {
            animator.SetBool("Hit3", false);
            noOfClicks = 0;
        }

        if(Time.time - lastClickedTime > maxComboDelay )
        {
            noOfClicks = 0;

        }
        if(Time.time > nextFireTime)
        {

            if(Input.GetMouseButtonDown(0) && hadTheSword)
            {
                OnClick();
            }
        }

        //gun state
        if (Input.GetKeyDown(KeyCode.Alpha2) && !isShooting)
        {
            ChangeAnimatorState();
            isShooting = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && isShooting)
        {
            animator.SetLayerWeight(1, 0);
            gun.SetActive(false);
            isShooting = false;
        }
        if (Input.GetMouseButtonDown(0) && HadTheGun() && inputY == 0 && isShooting)
        {
            animator.SetTrigger("Shoot");
           //light.SetActive(true);
            Shoot();
        }
        else
        {
            Debug.Log("Equipaggia prima l'arma");
        }
        if(inputY == 0)
        {
            mirino.SetActive(true);
        }
        else
        {
            mirino.SetActive(false);
        }

    }

    private void ChangeAnimatorState()
    {
        bool hadGun = HadTheGun();
        if (hadGun)
        { 
            animator.SetLayerWeight(1, 1);
           
            gun.SetActive(true);
         
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

        if (count == 0)
            return false;
        else return true;
    }

    void Shoot()
    {

        if (isShooting)
        {
            RaycastHit hit;
            if (Physics.Raycast(orientation.transform.position, orientation.transform.forward, out hit, range))
            {
                Instantiate(fireVFX, hit.point, Quaternion.LookRotation(hit.normal));
            }

        }
    }



    IEnumerator ChangeSword()
    {

        yield return new WaitForSeconds(1f);

        handSword.SetActive(true);
        backSword.SetActive(false);

    }

    IEnumerator SheathSword()
    {
        yield return new WaitForSeconds(1f);

        handSword.SetActive(false);
        backSword.SetActive(true);
        animator.SetLayerWeight(1, 0);
        animator.SetLayerWeight(2, 0);

    }

}
