using Ragdoll;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class UnityPulledZombie : MonoBehaviour
{


   
    private ObjectPool<UnityPulledZombie> pool;
    RagdollEnabler enable;
    private NavMeshAgent agent;
    [HideInInspector] public static bool bang = false;
    public GameObject fire;
    private ZombieHealth zombieHealth;
    [SerializeField] private GameObject cristal;
    private Collider capsuleCollider;
    [SerializeField] private GameObject skinZombie;
    [SerializeField] private Animator animator;
    [SerializeField] private Health healthBar;
    private EnemyNavMesh enemyNavMesh;
    private float attackTime = 1f;
    private float deathTime = 1f;

    private void Start()
    {
        enable = GetComponentInChildren<RagdollEnabler>();
        agent = GetComponentInChildren<NavMeshAgent>();
        zombieHealth = GetComponent<ZombieHealth>();
        capsuleCollider = GetComponent<Collider>();
        animator = GetComponentInChildren<Animator>();
        enemyNavMesh = GetComponentInChildren<EnemyNavMesh>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            fire.SetActive(true);
            fire.transform.localEulerAngles = transform.localEulerAngles;
            enable.EnableRagdoll();
            bang = true;
            StartCoroutine(KillTheZombie());
            foreach (Rigidbody rb in enable.rigidBodies)
            {
                if (deathTime > 0)
                {
                    deathTime -= Time.deltaTime;
                    rb.AddForce(new Vector3(2f, 5f, -230f));
                    rb.velocity = Vector3.zero;

                }

                if (deathTime == 0)
                {

                    rb.AddForce(new Vector3(0, 0, 0));

                }



                //agent.enabled = false;
                // pool.Release(other.TryGetComponent<UnityPulledZombie>(out UnityPulledZombie other.g))

            }
          //  healthBar.UpdateHealthBar(100, 0);


            //Destroy(other.gameObject);
            agent.isStopped = true;
        }
        if (other.gameObject.tag == "Sword")
        {

            if (zombieHealth.currentHealth > 0)
            {
                zombieHealth.currentHealth -= 10;
                animator.SetTrigger("Hit");
            }


            if (zombieHealth.currentHealth <= 0)
            {
                animator.SetTrigger("Death");
                Destroy(gameObject, 5f);
            }
            healthBar.UpdateHealthBar(100, zombieHealth.currentHealth);
        }


        if (other.gameObject.tag == "Player")
        {
            Attack();
        }

        if (other.CompareTag("PugnoR"))
        {
            enable.EnableRagdoll();
            foreach (Rigidbody rb in enable.rigidBodies)
            {


                rb.AddForce(new Vector3(200f, 500f, -230f));
                

            }

            enemyNavMesh.enabled = false;

        }
    }


    private void Update()
    {
        
    }

    public void SetPool(ObjectPool<UnityPulledZombie> pool)
    {
        this.pool = pool;
    }


    IEnumerator KillTheZombie()
    {
        WaitForSeconds wait = new WaitForSeconds(10);
        yield return wait;
        Instantiate(cristal.gameObject, skinZombie.transform.position, Quaternion.identity);
        bang = false;
        enable.EnableAnimator();
        
        enemyNavMesh.enabled = true;
        deathTime = 1;
        capsuleCollider.gameObject.SetActive(true);
        //healthBar.UpdateHealthBar(100, 100);
        fire.SetActive(false);
        pool.Release(this);
       
        
    }



    private void Attack()
    {
       
        attackTime -= Time.deltaTime;
        if (attackTime <= 0 )
        {

            animator.SetTrigger("Attacco Zombie");
            attackTime = 1f;
        }
    }
}
