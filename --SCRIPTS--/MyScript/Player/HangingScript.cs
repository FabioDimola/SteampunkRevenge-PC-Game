using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HangingScript : MonoBehaviour
{
    private Animator animator;
    private ThirdPersonController playerController;
    private GameObject player;
    
    public bool isHanging = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController=player.GetComponent<ThirdPersonController>();
        animator=player.GetComponent<Animator>();
    }


    private void Update()
    {
        if(playerController.Grounded && !isHanging)
        {
            animator.SetLayerWeight(4, 0);
        }

        if(isHanging)
        {
            playerController.Grounded = true;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                playerController.Gravity = -12;
                playerController.JumpAndGravity();
               
                isHanging = false;

                animator.SetLayerWeight(4, 0);
            }
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") && !playerController.Grounded)
        {
            Vector3 hangPos = other.transform.position;
           // player.transform.position = hangPos;
            animator.SetLayerWeight(4, 1);
            playerController._verticalVelocity = 0;
            playerController.Gravity = 0;
            isHanging = true;
        }

      
    }



   

}
