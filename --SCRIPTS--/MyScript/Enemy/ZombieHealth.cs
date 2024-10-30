using Ragdoll;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float maxExp = 100;
    public float currentHealth;
    private float currentExp;
    RagdollEnabler enable;
    private NavMeshAgent agent;

    public Health healthBar;
    public EnemyScriptable enemyScriptable;


    private Animator animator;
    public VisualEffect VFX_Graphs;
    public static ZombieHealth instance;
    private ZombiePatroling zombiePatroling;
    private SpellElectricity spellElectricity;

    private void Awake()
    {
        if (instance == null)
            instance = this;

    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        enable = GetComponentInChildren<RagdollEnabler>();
        zombiePatroling = GetComponent<ZombiePatroling>();
        currentHealth = 100;
        currentExp = 0;
        spellElectricity = FindAnyObjectByType<SpellElectricity>();
        VFX_Graphs.Stop();
        //  healthBar.UpdateHealthBar(maxHealth, currentHealth);
        agent = GetComponentInChildren<NavMeshAgent>();

    }




    private void Update()
    {
        if (currentHealth <= 0)
        {
            animator.SetTrigger("Death");
            Destroy(this.gameObject, 4f);
        }

    }


    public void PlayBlood()
    {
        VFX_Graphs.Play();
    }

  

    public void TakeDamage(float damage)
    {
        if(currentHealth > 0)
        {
            currentHealth -= damage;
           // healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }
    }
}