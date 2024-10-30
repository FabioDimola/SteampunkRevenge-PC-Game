
using Ragdoll;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;



public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
  

    private AISensor sensor;



    public float stopDistance = 2f;
   [HideInInspector] public Animator animator;

    private GameObject player; //riferimento testa giocatore
    private Vector3 playerPosition;
    //spell shoot
    public Transform spellShotPoint;


    public bool isDeath = false;
    public bool isThrowing = false;
    public GameObject objectToThrow;
   
    public Transform spawnObjectPoint;
    private Quaternion localRotationObj;

    private OnCollisionEnterDeath[] onCollisionEnterDeath;



    private Transform target;
    public float radiusAroundTarget;




    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        onCollisionEnterDeath = GetComponentsInChildren<OnCollisionEnterDeath>();
       
        stopDistance = Random.Range(3, 10);

        sensor = GetComponent<AISensor>();



        //the object will spawn  with the same rotation of the initial point
        localRotationObj = objectToThrow.transform.localRotation;
       SetUpRaggDoll();
    }










    private void LookTarget()
    {
        Vector3 lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }







    public void MoveTo()
    {
        int i = Random.Range(0, 360);
        int count = Random.Range(0, 360);


        agent.SetDestination(new Vector3(
        player.transform.position.x + radiusAroundTarget * Mathf.Cos(2 * Mathf.PI * i / count),
        player.transform.position.y,
        player.transform.position.z + radiusAroundTarget * Mathf.Sin(2 * Mathf.PI * i / count)

                ));
    }


    private void Update()
    {
       // playerPosition = player.transform.position;
       
        LookTarget();

     //   this.transform.LookAt(player.transform.position);
        float distance = Vector3.Distance( player.transform.position, transform.position);

        if (distance < stopDistance && !isDeath)
        {
            agent.isStopped = true;

            animator.SetBool("Shoot",true);
        }
        else if(distance > stopDistance)
        {
            
            animator.SetBool("Shoot",false);
            
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);

           
        }

       if(PlayerHealth.instance.currentHealth <= 0)
        {
            Dead(transform.position);
        }
       

       // Time.fixedDeltaTime = initialTimeFixedDeltaTime * Time.timeScale;
    }









    public void ThrowObject()
    {
       isThrowing = true;
        objectToThrow.transform.localRotation = localRotationObj;

        // separate the object from the parent
        objectToThrow.transform.parent = null;
        objectToThrow.SetActive(true);
        float distance = Vector3.Distance(player.transform.position, objectToThrow.transform.position);
        Rigidbody rb = objectToThrow.GetComponent<Rigidbody>();
        if (distance > 3)
        {
            rb.velocity = BalisticVelocity(spawnObjectPoint.transform.position, player.transform.position, 65);
            rb.angularVelocity = Vector3.zero;
        }
       
    }



    Vector3 BalisticVelocity(Vector3 source,Vector3 target, float angle)
    {
        Vector3 direction = target - source;
        float h = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        float a = angle * Mathf.Deg2Rad;
        direction.y = distance * Mathf.Tan(a);
        distance += h / Mathf.Tan(a);

        //Calculate the velocity
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * direction.normalized;
    }



    public void EnemySpellShoot()
    {
      //  BasicSpell spawnedSpell = Instantiate(enemySpell);
        
        Vector3 playerHeadPosition = player.transform.position - Random.Range(0.6f,1.3f) * -Vector3.up;

      //  var spawnedSpell = BulletManager.Instance.GetBullet();
      //  spawnedSpell.Initialize(spellShotPoint);
        spellShotPoint.up = (playerHeadPosition - spellShotPoint.position).normalized;
      //  spawnedSpell.gameObject.SetActive(true);
    }


    public void SetUpRaggDoll()
    {
        foreach(var item in GetComponentsInChildren<Rigidbody>())
        {
            item.isKinematic = true;
        }
    }


    public void Dead(Vector3 hitPosition)
    {
        isDeath = true;
        animator.enabled = false;

       
        this.transform.LookAt(Vector3.up);
        Debug.Log("EnemyDeve morire");
        foreach (var item in GetComponentsInChildren<Rigidbody>())
        {
            item.isKinematic = false;
        }

        foreach (var item in Physics.OverlapSphere(hitPosition,0.3f))
        {
           Rigidbody rb = item.GetComponent<Rigidbody>();

            if (rb)
            {
                rb.AddExplosionForce(1000,hitPosition,0.3f);
            }
        }

        foreach(OnCollisionEnterDeath collision in onCollisionEnterDeath)
        {
            if(!collision.hitByWeapon)
            {
                ThrowObject();
               
                
            }
        }
       
        agent.enabled = false;
        
       this.enabled = false;

       


    }



  
   
}
