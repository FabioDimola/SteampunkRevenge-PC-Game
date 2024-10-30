using Ragdoll;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDamage : MonoBehaviour
{

    RagdollEnabler enable;
    private NavMeshAgent agent;
    private Animator animator;
    private float attackTime = 1f;
    private ZombiePatroling zombiePatroling;
    private GameObject player;
    private Collider _collider;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enable = GetComponentInChildren<RagdollEnabler>();
        agent = GetComponent<NavMeshAgent>();
       zombiePatroling = GetComponent<ZombiePatroling>();
        _collider = GetComponent<Collider>();
        animator = GetComponentInChildren<Animator>();
       // enemyNavMesh = GetComponentInChildren<EnemyNavMesh>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
          
            enable.EnableRagdoll();
            
            //StartCoroutine(KillTheZombie());
            foreach (Rigidbody rb in enable.rigidBodies)
            {
              
                    rb.AddForce(new Vector3(2f, 5f, -230f));
                    rb.velocity = Vector3.zero;
               



                //agent.enabled = false;
                // pool.Release(other.TryGetComponent<UnityPulledZombie>(out UnityPulledZombie other.g))

            }
            //  healthBar.UpdateHealthBar(100, 0);

            zombiePatroling.enabled = false;
            //Destroy(other.gameObject);
            agent.isStopped = true;
        }
        if (other.gameObject.tag == "Sword")
        {

                animator.SetTrigger("Hit");
            


           
           
        }


        if (other.gameObject.tag == "Player")
        {
            Attack();
        }

        if (other.CompareTag("PugnoR") )
        {
            enable.EnableRagdoll();
            foreach (Rigidbody rb in enable.rigidBodies)
            {


                rb.AddForce(new Vector3(0, 0,player.transform.position.z * 600));


            }
            _collider.enabled = false;
            zombiePatroling.enabled = false;

        }
    }


    private void Attack()
    {

        attackTime -= Time.deltaTime;
        if (attackTime <= 0)
        {

            animator.SetTrigger("Attacco Zombie");
            attackTime = 1f;
        }
    }

    IEnumerator KillTheZombie()
    {
        WaitForSeconds wait = new WaitForSeconds(10);
        yield return wait;
       // Instantiate(cristal.gameObject, skinZombie.transform.position, Quaternion.identity);
        
        enable.EnableAnimator();

       
     


    }
}
