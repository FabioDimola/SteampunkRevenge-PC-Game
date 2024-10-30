using Ragdoll;
using StarterAssets;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;


public class ZombiePatroling : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private List<Enemy> enemyList;
    [SerializeField] private Transform[] position;
    [SerializeField] private float speed = 1f;
    private Coroutine WalkingCoroutine;



    [Header("Player Check Sphere")]
    [SerializeField] private float _playerCheckRadius = 5f;
    [SerializeField] private Vector3 _playerCheckOffset;
    [SerializeField] private LayerMask _playerMask;
    private bool _isInView = false;


  
    private float timeWaitAttack = 2f;

    [HideInInspector] public AISensor sensor;
    public float distanceFromP;

    private GameObject player;
    private Animator animator;
    private ZombieHealth enemyHealth;
    private bool _isAlive;
    private float velocity;
    private float shootingTime = 1f;
    private ThirdPersonController playerController;


    RagdollEnabler enable;
    private float attackTime = 1f;
    private float deathTime = 1f;
    [HideInInspector] public static bool bang = false;
    private EnemyNavMesh enemyNavMesh;

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
        enable = GetComponentInChildren<RagdollEnabler>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent.speed = speed;
        agent.isStopped = false;
        sensor = GetComponent<AISensor>();
       // enemyHealth = GetComponent<ZombieHealth>();
        animator = GetComponentInChildren<Animator>();
        playerController = player.GetComponent<ThirdPersonController>();
        enemyNavMesh = GetComponentInChildren<EnemyNavMesh>();
    }

    private void Update()
    {

        CheckPlayer();
      //  CheckEnemyLife();
        LookingPlayer();
        CheckAnimatorState();



     //   if (enemyHealth.currentHealth <= 0)
     //   {
    //        Stop();
           
             //StopCoroutine(WalkingCoroutine);
      //  }


        if (_isInView)
        {
            

        }
        else
        {
            Restart();
            
        }
    }



  

    private void LookingPlayer()
    {
         distanceFromP = Vector3.Distance(gameObject.transform.position, player.transform.position);


        if (_isInView && playerController.Grounded /*&& enemyHealth.currentHealth > 0*/ )
        {

            agent.SetDestination(player.transform.position);

            agent.transform.LookAt(player.transform.position);


            agent.speed = 2f;


            if (WalkingCoroutine != null)
            {
                StopCoroutine(WalkingCoroutine);
                animator.SetBool("Walk", false);
                WalkingCoroutine = null;


            }


            if (distanceFromP <= 3f)
            {
                Stop();
             
                animator.SetTrigger("Attacco Zombie");
                timeWaitAttack -= Time.deltaTime;
                if (timeWaitAttack <= 0)
                {
                    animator.SetTrigger("Attaco Zombie");
                    timeWaitAttack = 2f;

                }
            }
            else
            {
                Restart();
            }


        }
        else
        {
            agent.speed = 2f;
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
        agent.speed = 3f;
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

        attackTime -= Time.deltaTime;
        if (attackTime <= 0)
        {

            animator.SetTrigger("Attacco Zombie");
            attackTime = 1f;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            //fire.SetActive(true);
          ///  fire.transform.localEulerAngles = transform.localEulerAngles;
            enable.EnableRagdoll();
            bang = true;
           
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
     /*   if (other.gameObject.tag == "Sword")
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

        */
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

            this.enabled = false;

        }
    }


   

}
