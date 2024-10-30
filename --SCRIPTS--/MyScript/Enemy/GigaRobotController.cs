using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GigaRobotController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float maxHealth = 100;
    NavMeshAgent agent;
    [SerializeField] private float maxExp = 100;
    public float currentHealth;

   
    [SerializeField] private Health healthBar;
    private RobotPatroling robotPatroling;

    private void Start()
    {
            animator = GetComponentInParent<Animator>();
            currentHealth = maxHealth;
            agent = GetComponentInParent<NavMeshAgent>();
             healthBar.UpdateHealthBar(maxHealth, currentHealth);
        robotPatroling = GetComponent<RobotPatroling>();
            
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            animator.SetTrigger("Death");
            
            //  enemyNavMesh.isDeath = true;

            agent.velocity = Vector3.zero;
            Destroy(robotPatroling);

            healthBar.gameObject.SetActive(false);


        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Projectile")
        {
            if(currentHealth > 0)
            {

                currentHealth -= 20;
               // animator.SetTrigger("Hit");
                healthBar.UpdateHealthBar(100, currentHealth);

            }
            if(other.gameObject.tag == "Sword")
            {
                if (currentHealth > 0)
                {

                    currentHealth -= 5;
                    // animator.SetTrigger("Hit");
                    healthBar.UpdateHealthBar(100, currentHealth);

                }
            }
           
        }

        if(other.gameObject.tag == "Player")
        {
            Debug.Log("pLAYER REched");
            animator.SetTrigger("Attack");
            agent.velocity = Vector3.zero;
        }
    }
}
