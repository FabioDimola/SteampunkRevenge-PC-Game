using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float maxExp = 100;
    public float currentHealth;
    private float currentExp;

   public Health healthBar;
   public  EnemyScriptable enemyScriptable;
    
   private EnemyPatroling enemyPatroling;
    [SerializeField] private GameObject healthBonusVFX;
    [SerializeField] private GameObject vedetta;

    public float startAttack = 0;

    private CombatSystem combatSystem;
    public static EnemyHealth instance;
    private Animator animator;
    private SpellElectricity spellElectricity;

    private void Awake()
    {
        if (instance == null)
            instance = this;

    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = enemyScriptable.maxLife;
        combatSystem = FindAnyObjectByType<CombatSystem>();
        currentExp = 0;
        spellElectricity = FindAnyObjectByType<SpellElectricity>();
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
        enemyPatroling = GetComponentInParent<EnemyPatroling>();
        
    }




    private void Update()
    {
      

       
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sword")
        {
            Debug.Log("Hit");
            animator.SetTrigger("Hit1");
            if (currentHealth > 0)
            {


                currentHealth -= 20;
                startAttack = 0;
            }
            else
            {
                animator.SetBool("Death", true);
  
                Destroy(enemyPatroling);
                StartCoroutine(BonusHealth());
            }
            healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }else if(other.gameObject.tag == "PugnoL" || other.gameObject.tag == "PugnoR")
        {
            if (combatSystem.isAttacking)
            {
                animator.SetTrigger("Hit1");
                if (currentHealth >= 10)
                {


                    currentHealth -= 10;


                    startAttack = 0;
                }
                else
                {
                    animator.SetBool("Death", true);

                    Destroy(enemyPatroling);
                    StartCoroutine(BonusHealth());
                }
            }
            if (spellElectricity.spellMode && spellElectricity.isAttacking)
            {
                if(currentHealth >= 100)
                {
                    currentHealth -= 100;
                }
                else
                {
                    currentHealth = 0;
                }
               
                animator.SetBool("Death", true);

                Destroy(enemyPatroling);
                StartCoroutine(BonusHealth());
            }

            healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }
        else if(other.gameObject.tag == "Projectile")
        {
            animator.SetTrigger("Hit1");
            if (currentHealth > 0)
            {


                currentHealth -= 25;
                startAttack = 0;
            }
            else
            {
                animator.SetBool("Death", true);
               
                Destroy(enemyPatroling);
                StartCoroutine(BonusHealth());
            }
            healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }else if(other.gameObject.tag == "Foot" && combatSystem.isAttacking)
        {
            animator.SetTrigger("Hit1");
            if (currentHealth >= 10)
            {


                currentHealth -= 10;
                startAttack = 0;
            }
            else
            {
                animator.SetBool("Death", true);

                Destroy(enemyPatroling);
                StartCoroutine(BonusHealth());
            }

            if (spellElectricity.spellMode && spellElectricity.isAttacking)
            {
                if(currentHealth >= 100)
                {
                    currentHealth -= 100;
                }
                else
                {
                    currentHealth = 0;
                }
               
                animator.SetBool("Death", true);

                Destroy(enemyPatroling);
                StartCoroutine(BonusHealth());
            }

            healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }
    }



    IEnumerator BonusHealth()
    {
        yield return new WaitForSeconds(6);
        Instantiate(healthBonusVFX, transform.position, Quaternion.identity);
        Destroy(vedetta);
    }
}
