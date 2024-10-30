using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ChestScript : MonoBehaviour
{

     public Animator chestAnim;
     public Animator lightAnim;
    
    
     public int pot=20;
    


  
    private int y = 30;
    private int z = -40;
    //private Vector3 oldPos;


   


    private void Update()
    {
    }
    IEnumerator OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {

            
            chestAnim.SetBool("Open", true);
            lightAnim.SetBool("Up", true);
           
            //ScoreManager.instance.scoreC.superPower += pot;
            yield return new WaitForSeconds(7f);
            
            
           
           


        }
    }

   
}
