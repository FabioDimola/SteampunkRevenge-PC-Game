using Ragdoll;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyNavMesh : MonoBehaviour
{
    NavMeshAgent agent;
    Transform ThePlayer;
    float distanceFromP;
    public float Tollerance;

    private GameObject Player;

    public Transform[] PosizioniPredefinite;
    private Coroutine WalkingCoroutine;
    
    bool stop = false;
    Animator animator;
    public float velocity;
    [Header("Player Check Sphere")]
    [SerializeField] private float _playerCheckRadius = 5f;
    [SerializeField] private Vector3 _playerCheckOffset;
    [SerializeField] private LayerMask _playerMask;
    private bool _isInView = false;
    PlayerHealth playerHealth;
    public bool isDeath = false;
    private int currentHealth = 100;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("Walk", true);
        playerHealth = FindObjectOfType<PlayerHealth>();
    }
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

    private void Update()
    {
        CheckPlayer();
        LookingPlayer();
      
      if(currentHealth <= 0)
        {
            animator.SetTrigger("Death");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sword")
        {
            currentHealth -= 25;
            animator.SetTrigger("Hit");
        }
    }

    private void LookingPlayer()
    {


        if (_isInView  /*&& enemyHealth.currentHealth > 0*/ )
        {

            agent.SetDestination(Player.transform.position);

            
            ///agent.transform.LookAt(Player.transform.position);



            if (WalkingCoroutine != null)
            {
                StopCoroutine(WalkingCoroutine);
                animator.SetBool("Walk", false);
                
                WalkingCoroutine = null;


            }


            if (distanceFromP <= 4f)
            {
                Stop();

                animator.SetTrigger("Attack");

               
            }
            else
            {
                Restart();
               
                // animator.SetBool("Run", false);
                if (WalkingCoroutine == null)
                {
                    WalkingCoroutine = StartCoroutine(RandomWalk());

                }

            }


        }
        else
        {
            agent.speed = 4f;
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
        
        agent.speed = 4f;
        animator.SetBool("Walk", true);
    }



    // Update is called once per frame
  
    public IEnumerator RandomWalk()
    {
        agent.SetDestination(PosizioniPredefinite[Random.Range(0, PosizioniPredefinite.Length)].position);
        yield return new WaitForSeconds(Random.Range(4, 20));
        WalkingCoroutine = null;
    }

    public void TakeDamagePlayer()
    {
        if(distanceFromP < 3)
        playerHealth.TakeDamage(20);
    }

   


    

  


}
