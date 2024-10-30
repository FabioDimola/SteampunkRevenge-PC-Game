using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatroling : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] private List<Enemy> enemyList;
    [SerializeField] private Transform[] position;
    [SerializeField] private float speed = 3f;
    private Coroutine WalkingCoroutine;



    [Header("Player Check Sphere")]
    [SerializeField] private float _playerCheckRadius = 5f;
    [SerializeField] private Vector3 _playerCheckOffset;
    [SerializeField] private LayerMask _playerMask;
    private bool _isInView = false;


    [Header("Shooter Input")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;

    private float timeWaitAttack = 2f;

    [HideInInspector] public AISensor sensor;
   

    private GameObject player;
    private Animator animator;
    private EnemyHealth enemyHealth;
    private bool _isAlive;
    private float velocity;
    private float shootingTime = 1f;
    private ThirdPersonController playerController;


    

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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent.speed = speed;
        agent.isStopped = false;
        sensor = GetComponent<AISensor>();
        enemyHealth = GetComponentInChildren<EnemyHealth>();
        animator = GetComponentInChildren<Animator>();
        
        playerController = player.GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
       
        CheckPlayer();
        CheckEnemyLife();
        LookingPlayer();
        CheckAnimatorState();

       
        if (!_isAlive)
        {
            Stop();
            
        }
        

        if(_isInView && enemyHealth.currentHealth > 0 )
        {
            Shoot();
            
        }
        else
        {
            Restart();
            animator.SetBool("Fire", false);
        }


        //if(enemyHealth.currentHealth <= 0)
       // {
       //     animator.SetBool("Death", true);
            
        //}
    }



    void Shoot()
    {
        Stop();
        animator.SetBool("Fire",true);
        
        shootingTime -= Time.deltaTime;
       if(shootingTime <=0 && playerController.Grounded)
       {

            Vector3 target = (player.transform.position - transform.position).normalized;
            Instantiate(bulletPrefab, spawnPoint.position, Quaternion.LookRotation(target, Vector3.up));
            shootingTime = 1.5f;
        }
        

    }

    private void LookingPlayer()
    {
       float distanceFromP = Vector3.Distance(gameObject.transform.position, player.transform.position);
       

        if (_isInView && playerController.Grounded && _isAlive)
        {

            agent.SetDestination(player.transform.position);

            agent.transform.LookAt(player.transform.position);


            agent.speed = 4f;
           

            if(WalkingCoroutine != null)
            {
                StopCoroutine(WalkingCoroutine);
                animator.SetBool("Walk", false);
                WalkingCoroutine = null;
                

            }

            
             if(distanceFromP <=2f)
            {
                Stop();
                animator.SetLayerWeight(1, 1);
                animator.SetTrigger("Hit");
                timeWaitAttack -= Time.deltaTime;
                if(timeWaitAttack <= 0)
                {
                    animator.SetTrigger("Attack");
                    timeWaitAttack = 2f;

                }
            }
            else
            {
                Restart();
                animator.SetLayerWeight(1, 0);
            }
            

        }
        else
        {
            agent.speed = 2f;
            if(WalkingCoroutine == null && _isAlive)
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
        
        if(enemyHealth.currentHealth  <= 0)
        {
            _isAlive = false;
        }
        else
        {
            _isAlive = true;
        }
       
    }

    private void Damage(GameObject obj)
    {
        float currentHealth;
        if (_isAlive)
        {
            currentHealth = enemyHealth.currentHealth;
            switch (obj.tag)
            {
                
                case "Projectile":
                        currentHealth -= 15f;
                    break;
                case "Sword":
                    currentHealth -= 5f;
                    break;
                case "Pugno":
                    currentHealth -= 10;
                    break;

            }
        }
    }


  


}