using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSwordAttack : MonoBehaviour
{

    //combat  combo
    int comboCounter=0;
    float coolDownTime = 0.1f;
    float lastClicked;
    float lastComboEnd;


    //character info
    [SerializeField] Weapon currentWeapon;
    Animator animator;





    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

       
       
      if(currentWeapon != null)
        {
            Debug.Log(currentWeapon.weaponName);
            Attack(currentWeapon.weaponName);
        }  
    }


    void Attack(string weapon)
    {
        if(Input.GetMouseButtonDown(0) && Time.time - lastComboEnd > coolDownTime)
        {
            comboCounter++;
            comboCounter = Mathf.Clamp(comboCounter, 0, currentWeapon.comboLenght);

            Debug.Log("Stai attaccando con spada");
            //create AttackNames
            for(int i = 0; i < comboCounter; i++)
            {
                 if(i==0)
                {
                    if(comboCounter == 1 && animator.GetCurrentAnimatorStateInfo(0).IsTag("Movement"))
                    {

                        animator.SetBool("AttackStart", true);
                        animator.SetBool(weapon + "Attack" + (i+1), true);
                        lastClicked = Time.time;

                    }
                }
                else
                {
                    if (comboCounter >= (i+1) && animator.GetCurrentAnimatorStateInfo(0).IsName(weapon + "Attack" + i))
                    {

                    
                        animator.SetBool(weapon + "Attack" + (i + 1), true);
                        animator.SetBool(weapon + "Attack" + i , true);
                        lastClicked = Time.time;

                    }

                }
            }
        }
       

        //animation exit bool reset
        for(int i = 0;i < comboCounter; i++)
        {
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsName(weapon + "Attack" + (i+1)))
            {
                comboCounter = 0;
                lastComboEnd = Time.time;
                animator.SetBool(weapon + "Attack" + (i + 1), false);
                animator.SetBool("AttackStart", false);
            }
        }

    }
}
