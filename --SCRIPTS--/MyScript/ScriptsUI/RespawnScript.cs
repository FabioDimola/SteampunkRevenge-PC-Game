using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
     private Vector3 _respawnPoint;
    //[SerializeField] private string[] _spawnableTags;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject deathPanel;
    private CharacterController characterController;
    [SerializeField] private GameObject handSword;
    [SerializeField] private GameObject backSword;


    private PlayerHealth health;
    private Animator animator;
   [SerializeField] private Health healthBar;


    private void Start()
    {
        //characterController = player.GetComponent<CharacterController>();
        _respawnPoint = transform.position;
        health = player.GetComponent<PlayerHealth>();
        
        animator = player.GetComponent<Animator>();
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_respawnPoint, 0.5f);
    }

    public void Respawn()
    {
        if (health.isDead)
        {
            characterController = player.GetComponent<CharacterController>();
            characterController.enabled = false;
           Cursor.lockState = CursorLockMode.Locked;
            
            animator.SetBool("Death", false);
            //backSword.SetActive(true);
            PlayerHealth.instance.currentHealth = 100;
            healthBar.UpdateHealthBar(100,100);
            animator.SetLayerWeight(2, 0);
            animator.SetLayerWeight(1, 0);
            handSword.SetActive(false);
            // il character controller se si sta muovendo da problemi a spostarlo in det posizioni prefissate
            player.transform.position = _respawnPoint;
            characterController.enabled = true;
            health.isDead = false;
            Time.timeScale = 1;
        }
       
    }



    
}
