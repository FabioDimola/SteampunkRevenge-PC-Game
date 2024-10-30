using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Teleport : MonoBehaviour
{

    private CharacterController controller;
    private Animator animator;
    public Transform[] teleportPosition;
    public Transform[] teleportLabPosition;
    public GameObject teleportPortal;
    public Transform teleportSpawnPos;
    public Transform teleportJumpPos;

    private GameObject player;
    private GameObject portalPref;
    private Transform enemyPos;
    private GameObject enemy;

   
    private GameObject mainCamera;

    public Vector3 _enemyCheckOffset;
    public float _enemyCheckRadius;
    public LayerMask _enemyMask;
    public bool _isInView = false;
    public Vector3 _teleportOffset;
    public GameObject teleportSphere;

    public GameObject playCam;
    public GameObject covoCam;
    public GameObject teleportAtCam;

    private bool isInLab = false;
    private SpellElectricity spellState;

    private ThirdPersonController thirdPersonController;
    public LayerMask teleportLayer;
    [SerializeField] private Transform debugTransform;


    [HideInInspector] public bool isTeleporting = false;
    [SerializeField] private Experience expBar;

    void Awake()
    {

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        spellState = GetComponent<SpellElectricity>();
        //rb = GetComponent<Rigidbody>(); 
        player = GameObject.FindGameObjectWithTag("Player");
        // m_EulerAngleVelocity = new Vector3(0,100,0);
        thirdPersonController = GetComponent<ThirdPersonController>();
    }


    private void OnDrawGizmos() //disegna sfera per determinare se il player è nell'area o no
    {
        //SFERA PlayerCHECK 
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_enemyCheckOffset, _enemyCheckRadius);

    }



    private void CheckEnemy()
    {
        _isInView = Physics.CheckSphere(transform.TransformPoint(_enemyCheckOffset), _enemyCheckRadius, _enemyMask);


    }

    private void FindEnemyNearPos()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.TransformPoint(_enemyCheckOffset),_enemyCheckRadius, _enemyMask);


        //prendo tutti i collider e controllo quello piu' vicino e prendo la pos
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if(Vector3.Distance(controller.transform.position, hitColliders[i].transform.position) <= 20)
            {
                if (Input.GetKeyDown(KeyCode.N))
                {

                    Debug.Log("teleport");
                    Debug.Log(hitColliders[i].transform.position);
                    controller.enabled = false;
                    controller.transform.position = hitColliders[i].gameObject.transform.position - _teleportOffset;
                }
            }
            
        }

      

    }

    public void EndTeleport()
    {
        
        StopCoroutine(StartTeleport());
        teleportSphere.SetActive(false);
        teleportAtCam.SetActive(false);
       // controller.enabled = true;
        //attacco
    }

    public void TeleportAttack()
    {
        if(Input.GetMouseButtonUp(1) && !spellState.spellMode)
        {
            if(PlayerHealth.instance.currentExp > 0 && _isInView)
            {
                animator.SetTrigger("Teleport");
                teleportAtCam.SetActive(true);
                teleportSphere.SetActive(true);
            }
           
            
        }
    }


    IEnumerator StartTeleport()
    {
        yield return new WaitForSeconds(1f);
        Collider[] hitColliders = Physics.OverlapSphere(transform.TransformPoint(_enemyCheckOffset), _enemyCheckRadius, _enemyMask);


        //prendo tutti i collider e controllo quello piu' vicino e prendo la pos
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (Vector3.Distance(controller.transform.position, hitColliders[0].transform.position) <= _enemyCheckRadius && PlayerHealth.instance.currentExp > 0)
            {
               

                    Debug.Log("teleport");
                    Debug.Log(hitColliders[i].transform.position);
                    controller.enabled = false;
                    
                    controller.transform.position = hitColliders[0].gameObject.transform.position - _teleportOffset;
                if(PlayerHealth.instance.currentExp >= 10)
                {
                    PlayerHealth.instance.currentExp -= 10;
                }
                
                expBar.UpdateExpBar(100,PlayerHealth.instance.currentExp);
                
                yield return new WaitForSeconds(0.2f);

                controller.enabled = true;
            }

        }

    }

    private void Update()
    {
        CheckEnemy();
       TeleportAttack();


        //covo teleport

        if (Input.GetKeyDown(KeyCode.T))
        {

           


          //  isTeleporting =  true;
            //mainCamera.SetActive(false);
           // Cursor.lockState = CursorLockMode.None;
           // teleportCamera.gameObject.SetActive(true);
            if(thirdPersonController.Grounded)
           portalPref =  Instantiate(teleportPortal.gameObject,teleportSpawnPos.position,player.transform.rotation);
           else
            portalPref = Instantiate(teleportPortal.gameObject, teleportJumpPos.position, player.transform.rotation);
        }


        //Attack Teleport



       /* if (isTeleporting && Input.GetMouseButtonDown(0))
        {
            TeleportPosition();
           
            mainCamera.SetActive(true) ;
            teleportCamera.gameObject.SetActive(false) ;



        }*/
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Teleport" && isInLab)
        {
            controller.enabled = false;
            
          //  if (enemy != null)
                controller.transform.position = teleportPosition[Random.Range(0, teleportPosition.Length)].position;
         //   else
             //   controller.transform.position = TeleportPosition();
            
        }else if(other.gameObject.tag == "Teleport" && !isInLab)
        {
            controller.transform.position = teleportLabPosition[Random.Range(0, teleportLabPosition.Length)].position;
        }

        if(other.gameObject.tag == "Covo")
        {
            playCam.SetActive(false );
        }
      

    }


   



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Teleport")
        {
            isTeleporting = false;
            Cursor.lockState = CursorLockMode.Locked;
            //  teleportPortal.SetActive(false);
            controller.enabled = true;
           if(portalPref != null)
            {
                Destroy(portalPref.gameObject);
            }
        }

        if(other.gameObject.tag == "Covo")
        {
            isInLab = false;
            playCam.SetActive(true);
            covoCam.SetActive(false) ;
        }




    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Teleport")
        {
            controller.enabled = false;

        }else if(other.gameObject.tag == "Covo")
        {
            isInLab = true;
            playCam.SetActive(false);
            covoCam.SetActive(true);
            
           // controller.enabled = true;
        }
        else
        {
            controller.enabled = true;
        }
    }
}
