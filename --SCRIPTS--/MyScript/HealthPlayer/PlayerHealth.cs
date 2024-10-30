
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;



public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float maxExp = 100;
    public float currentHealth;
    public float currentExp;

    public  Health healthBar;
    [SerializeField] private Experience expBar;
   [HideInInspector] public Animator animator;
    public bool isDead = false;
    private RespawnScript respawn;


    [SerializeField] SuperPower superPowerBar;
    public float currentPower = 0;
    public float maxPower = 100;

    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject menuCanvas;

    public bool menuActive = false;
    public static PlayerHealth instance;
    public GameObject hitPanel;

    public AudioSource powerItemSX;
    public AudioSource gameOverSX;
    private void Awake()
    {
        if (instance == null)
            instance = this;
       
    }

    private void Start()
    {

        currentHealth = maxHealth;
        currentExp = 0;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
        expBar.UpdateExpBar(maxExp, 0);
        superPowerBar.UpdatePowBar(maxPower, 0);
        animator = GetComponentInParent<Animator>();
    }


    

    private void Update()
    {
        

        if(currentHealth <= 0 && !isDead)
        {
           // animator.SetLayerWeight(3, 1);
            animator.SetTrigger("Death");
            StartCoroutine(DeadPanel());
            isDead = true;
            gameOverSX.Play();

            Time.timeScale = 0;
        }



    }

    private void OnTriggerEnter(Collider other)
    {
     
        if(other.gameObject.tag == "TeleportItem")
        {
            if (currentExp < 100)
            {
                currentExp += 10;
                Destroy(other.gameObject);
                powerItemSX.Play();
            }
            expBar.UpdateExpBar(maxExp, currentExp);
        }

        if (other.gameObject.tag == "ElectricItem")
        {
            if (currentPower < 100)
            {
                currentPower += 10;
                Destroy(other.gameObject);
                powerItemSX.Play();
            }
            superPowerBar.UpdatePowBar(maxPower, currentPower);
        }

        if (other.gameObject.tag == "ProjectileEnemy")
        {
            if (currentHealth > 0)
            {

                animator.SetTrigger("Hit1");
                TakeDamage(4f);
            }

           
        }

        if(other.gameObject.name == "HammerR" || other.gameObject.name == "HammerL")
        {
            animator.SetTrigger("Hit1");
        }

        
        if (other.gameObject.tag == "Health")
        {
            if(currentHealth < 100)
            {
                currentHealth += 10;
                Destroy(other.gameObject, 2f);
            }
            healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }

       if(other.gameObject.name == "MenuUI")
        {
            menuCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            menuActive = true;
        }
      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "MenuUI")
        {
            menuCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            menuActive = false;
        }

    }

    public void Cure()
    {
        if (currentHealth > 0)
        {
            

            currentHealth +=10;
            healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Pugno R")
        {
            if (currentHealth > 0)
            {


                currentHealth -= 50;
                animator.SetTrigger("Punch");
            }
            healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }

        
    }


    IEnumerator DeadPanel()
    {

        yield return new WaitForSeconds(4f);
        Time.timeScale = 0f;
        //currentHealth = maxHealth;
        Cursor.lockState = CursorLockMode.None;
       // healthBar.UpdateHealthBar(maxHealth, currentHealth);

        deathPanel.SetActive(true);
        

    }


    public void TakeDamage(float damage)
    {
        if (currentHealth > 0)
        {
            hitPanel.SetActive(true);
            animator.SetTrigger("Hit1");
            StartCoroutine(DisableHitPanel());
            
            currentHealth -= damage;
            healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }


    }

    IEnumerator DisableHitPanel()
    {
        yield return new WaitForSeconds(0.5f);
        hitPanel.SetActive(false);
    }
}
