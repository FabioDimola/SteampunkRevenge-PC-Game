using Ragdoll;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZombieMoveTest : MonoBehaviour
{

   private RagdollEnabler enable;
   private  Animator animator;
   private CharacterController characterController;
    private CombatSystem playerCombatSystem;
   private float moveSpeed = 2.0f;
   private Vector3 moveDirection;
   private bool isMoving = false;
   private GameObject player;
   public GameObject zombieHealth;
    private ZombieHealth health;

    bool strafeL = false;
    bool strafeR = false;
   private Coroutine MovementCoroutine;

    public Rigidbody rbBodyZ;

    // Start is called before the first frame update
    void Start()
    {
        enable = GetComponentInChildren<RagdollEnabler>();
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponentInChildren<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCombatSystem = player.GetComponent<CombatSystem>();
        MovementCoroutine = StartCoroutine(EnemyMovement());
       health = GetComponentInParent<ZombieHealth>();

    }

    // Update is called once per frame
    void Update()
    {

        //Constantly look at player
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        //Only moves if the direction is set
        MoveEnemy(moveDirection);




    }

    IEnumerator EnemyMovement()
    {
        //Waits until the enemy is not assigned to no action like attacking or retreating
        yield return new WaitForSeconds(2);

        int randomChance = Random.Range(0, 2);

        if (randomChance == 1)
        {
            int randomDir = Random.Range(0, 2);
            moveDirection = randomDir == 1 ? Vector3.right  : Vector3.left;
            isMoving = true;
        }
        else
        {
            StopMoving();
        }

        yield return new WaitForSeconds(1);

        MovementCoroutine = StartCoroutine(EnemyMovement());
    }




    void MoveEnemy(Vector3 direction)
    {
        //Set movespeed based on direction
        moveSpeed = 1;

        if (direction == Vector3.forward)
            moveSpeed = 5;
      
        if (direction == -Vector3.forward)
            moveSpeed = 2;

        //Set Animator values
       
        animator.SetBool("StrafeR", direction == Vector3.right);
        animator.SetBool("StrafeL", direction == Vector3.left);

    
       

        //Don't do anything if isMoving is false
        if (!isMoving)
            return;

        Vector3 dir = (player.transform.position - transform.position).normalized;
        Vector3 pDir = Quaternion.AngleAxis(90, Vector3.up) * dir; //Vector perpendicular to direction
        Vector3 movedir = Vector3.zero;

        Vector3 finalDirection = Vector3.zero;

        if (direction == Vector3.forward)
            finalDirection = dir;
        if (direction == Vector3.right || direction == Vector3.left)
            finalDirection = (pDir * direction.normalized.x);
        if (direction == -Vector3.forward)
            finalDirection = -transform.forward;

        if (direction == Vector3.right || direction == Vector3.left)
            moveSpeed /= 1.5f;

        movedir += finalDirection * moveSpeed * Time.deltaTime;

        characterController.Move(movedir);



        /*
         *  if (!isPreparingAttack)
              return;
         * 
         * if (Vector3.Distance(transform.position, player.transform.position) < 2)
          {
              StopMoving();
              if (!playerCombat.isCountering && !player.isAttackingEnemy)
                  Attack();
              else
                  PrepareAttack(false);
          }
        */
    }


    public void StopMoving()
    {
        isMoving = false;
        moveDirection = Vector3.zero;
        if (characterController.enabled)
            characterController.Move(moveDirection);
    }





    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PugnoL") || other.CompareTag("PugnoR"))
        {
            //health.TakeDamage(20);

         //   animator.SetTrigger("HitPunch");


        /* if(playerCombatSystem.animator.GetCurrentAnimatorStateInfo(0).IsName("PunchAttack2") )
            {
                enable.EnableRagdoll();
                characterController.enabled = false;
             //  this.enabled = false;
                rbBodyZ.velocity = transform.up * 50;
             //   zombieHealth.SetActive(false);

                //StartCoroutine(HitPunch());
            }
           
            
           */
        }



        if(other.name == "Effect15_Collision")
        {
            
            enable.EnableRagdoll();
            characterController.enabled = false;
            this.enabled = false;
            rbBodyZ.velocity = -transform.forward * 100;
            zombieHealth.SetActive(false);

          //  StartCoroutine(HitPunch());
        }
    }



    IEnumerator HitPunch()
    {
        yield return new WaitForSeconds(4);

        enable.EnableAnimator();
        animator.SetTrigger("Reactive");
        characterController.enabled = true;
        characterController.transform.position = transform.position;
        
        zombieHealth.SetActive(true);
        this.enabled = true;
        
       
    }
}
