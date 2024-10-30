using Ragdoll;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private NavMeshAgent agent;
    
    [SerializeField] private Transform[] position;
    [SerializeField] private float speed = 1f;
    private Coroutine WalkingCoroutine;



    [Header("Player Check Sphere")]
    [SerializeField] private float _playerCheckRadius = 5f;
    [SerializeField] private Vector3 _playerCheckOffset;
    [SerializeField] private LayerMask _playerMask;
    private bool _isInView = false;



    private float timeWaitAttack = 2f;

  
    public float distanceFromP;
    private CombatSystem combatSystem;
    private GameObject player;
    private Animator animator;
    private ZombieHealth enemyHealth;
    private bool _isAlive;
    private float velocity;
    private float shootingTime = 1f;
    private ThirdPersonController playerController;
    private SpellElectricity spellElectricity;

    
    private float attackTime = 1f;
    private float deathTime = 1f;
  
    private EnemyNavMesh enemyNavMesh;
    private Material skinnedMaterial;
    public SkinnedMeshRenderer skinnedMesh;

    private void OnDrawGizmos() //disegna sfera per determinare se il player è nell'area o no
    {
        //SFERA PlayerCHECK 
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_playerCheckOffset, _playerCheckRadius);

    }

    private void CheckPlayer()
    {
        _isInView = Physics.CheckSphere(transform.TransformPoint(_playerCheckOffset), _playerCheckRadius, _playerMask);


    }

    private void Start()
    {
        combatSystem = FindAnyObjectByType<CombatSystem>();
        spellElectricity = FindAnyObjectByType<SpellElectricity>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent.speed = speed;
        agent.isStopped = false;
      
        enemyHealth = GetComponent<ZombieHealth>();
        animator = GetComponent<Animator>();
        playerController = player.GetComponent<ThirdPersonController>();
        enemyNavMesh = GetComponentInChildren<EnemyNavMesh>();
        Restart();

        if(skinnedMesh != null)
        {
            skinnedMaterial = skinnedMesh.sharedMaterial;
        }
    }

    private void Update()
    {

        CheckPlayer();
        //  CheckEnemyLife();
        LookingPlayer();
     CheckAnimatorState();


        if(skinnedMaterial.GetFloat("_DissolveAmmount") >= 0.69f)
        {
            skinnedMaterial.SetFloat("_DissolveAmmount", 0);
        }

        distanceFromP = Vector3.Distance(gameObject.transform.position, player.transform.position);


        if (_isInView && distanceFromP > 2)
        {
            animator.SetBool("Run", true);
            agent.SetDestination(player.transform.position);
            agent.speed = 5f;

        }else if(distanceFromP <= 2)
        {
            animator.SetBool("Run", false);
            agent.isStopped=true;
           // Attack();
        }
        
    }

   


    private void LookingPlayer()
    {
       

        if (_isInView && playerController.Grounded /*&& enemyHealth.currentHealth > 0*/ )
        {

            agent.SetDestination(player.transform.position);

          //  agent.transform.LookAt(player.transform.position);
          
            

            if (WalkingCoroutine != null)
            {
                StopCoroutine(WalkingCoroutine);
                //animator.SetBool("Walk", false);
                animator.SetBool("Run", false);
                WalkingCoroutine = null;


            }


            if (distanceFromP <= 2f && PlayerHealth.instance.currentHealth >=0)
            {
                Stop();

               animator.SetTrigger("Attacco Zombie");
               
             /*   timeWaitAttack -= Time.deltaTime;
                if (timeWaitAttack <= 0 && PlayerHealth.instance.currentHealth > 0)
                {
                    animator.SetTrigger("Attaco Zombie");
                    //PlayerHealth.instance.TakeDamage(10);
                    timeWaitAttack = 0.8f;

                }*/
            }
            else
            {
                Restart();
               
            }


        }
        else
        {
            agent.speed = 0.9f;
           // animator.SetBool("Run", false);
            if (WalkingCoroutine == null)
            {
                WalkingCoroutine = StartCoroutine(RandomWalk());

            }

        }

    }

    public void Stop()
    {
        agent.isStopped = true;
        
        agent.speed = 0;
    }

    private void Restart()
    {

        agent.isStopped = false;
        animator.SetBool("Walk", true);
        agent.speed = 0.5f;
    }



    public IEnumerator RandomWalk()
    {
        Transform destination = position[Random.Range(0, position.Length)];
        agent.SetDestination(destination.position);
        agent.transform.LookAt(destination.position);


        yield return new WaitForSeconds(Random.Range(4, 20));
        WalkingCoroutine = null;
    }

    private void CheckAnimatorState()
    {

        velocity = agent.velocity.z;
        if (velocity != 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

    }

    private void CheckEnemyLife()
    {
        Debug.Log(enemyHealth.currentHealth);
        if (enemyHealth.currentHealth <= 0)
        {
            _isAlive = false;
        }
        else
        {
            _isAlive = true;
        }

    }

    private void Attack()
    {
        if (PlayerHealth.instance.currentHealth > 0)
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0 && PlayerHealth.instance.currentHealth > 0)
            {

                animator.SetTrigger("Attacco Zombie");
                attackTime = 1f;
            }
        }
        else
        {
            Stop();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
       
      

        if (other.CompareTag("PugnoR") || other.CompareTag("Foot") || other.gameObject.tag == "Sword" || other.gameObject.name == "VFX_ChargeElectricity")  
        {
            if(combatSystem.isAttacking || spellElectricity.isAttacking)
            {
                animator.SetTrigger("Hit");
                enemyHealth.currentHealth -= 20;
                if (spellElectricity.spellMode && spellElectricity.isAttacking)
                {
                    animator.SetTrigger("Death");
                  
                    Destroy(this.gameObject, 4f);
                }
                this.enabled = false;
            }


        }

      

       
    }


   


}
