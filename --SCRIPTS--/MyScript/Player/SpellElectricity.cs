using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpellElectricity : MonoBehaviour
{

    Animator animator;
    public GameObject electricityVFX;
    public VisualEffect effectTrial;
    public float maxLifeTime = 0.4f;
    public float maxSpeed = 40f;
    private float startLifeTime;
    private float startSpeed;
    private float startRadius = 0;
    public bool spellMode = false;
    public SuperPower powerBar;
    private float currentLifeTime = 0;

    [HideInInspector] public bool isAttacking = false;

    public GameObject electricVFXFoot;
    private float startLifeTimeFoot;
    private CombatSystem combatSystem;

    public AudioSource electricLoop;
   
    // Start is called before the first frame update
    void Start()
    {
       if(effectTrial != null)
        {
            effectTrial.Stop();
            startLifeTime = electricityVFX.GetComponent<VisualEffect>().GetFloat("LifeTime");
            electricVFXFoot.GetComponent<VisualEffect>().SetFloat("LifeTime", 0);
            startSpeed = electricityVFX.GetComponent<VisualEffect>().GetFloat("Speed");
        }
       
        animator = GetComponent<Animator>();
        combatSystem = GetComponent<CombatSystem>();
        
      //  startRadius = electricityVFX.GetComponent<VisualEffect>().GetFloat("baseRadius");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha3) && PlayerHealth.instance.currentPower >=5)
        {
            animator.SetTrigger("StartSpell");
            if(PlayerHealth.instance.currentPower >=10)
            {
                PlayerHealth.instance.currentPower -= 10;
            }
            
           CircleElectricity();
            powerBar.UpdatePowBar(100,PlayerHealth.instance.currentPower);
        }


        if(spellMode)
        {
            currentLifeTime = electricityVFX.GetComponent<VisualEffect>().GetFloat("LifeTime");
            if (Input.GetMouseButton(1))
            {
                ElectricAttack();
            }
            if (Input.GetMouseButtonUp(1) && currentLifeTime == maxLifeTime)
            {
                animator.SetTrigger("SpellAttack");
               electricityVFX.GetComponent<VisualEffect>().SetFloat("LifeTime",startLifeTime);
                Debug.Log(currentLifeTime);
            }
            if(Input.GetMouseButtonUp(0) && currentLifeTime == maxLifeTime)
            {
                animator.SetTrigger("PowerAttack");
                isAttacking = true;
            }

            if (Input.GetKeyDown(KeyCode.K) && spellMode )
            {
                animator.SetTrigger("Kick");
                electricVFXFoot.SetActive(true);
                electricVFXFoot.GetComponent<VisualEffect>().SetFloat("LifeTime", Mathf.Lerp(0, 4, 3f));
                animator.SetFloat("PowerKick", electricVFXFoot.GetComponent<VisualEffect>().GetFloat("LifeTime"));


                isAttacking = true;

            }

          
        }
    }


    public void CircleElectricity()
    {
        effectTrial.Play();
        effectTrial.GetComponent<VisualEffect>().SetFloat("baseRadius", Mathf.Lerp(startRadius, 3f, 1f));
    }

    public void FinishAttack()
    {
        isAttacking = false;
    }

    //stoppa cerchio elettrico

    public void StopElectricVFX()
    {
        effectTrial.Stop();
    }

    public void ResetFootElectricity()
    {
        electricVFXFoot.GetComponent<VisualEffect>().SetFloat("LifeTime", 0);
        isAttacking = false;
    }

    //attiva elettricit mano
    public void ActiveElectricity()
    {
        electricityVFX.SetActive(true);
    }

    public void SpellLayer()
    {
        animator.SetLayerWeight(5, 1);
        electricLoop.Play();
        spellMode = true;

    }




    private void ElectricAttack()
    {
      

        electricityVFX.GetComponent<VisualEffect>().SetFloat("LifeTime", Mathf.Lerp(startLifeTime, maxLifeTime, 4));
        //se lifetime ==4 animazione spell piu aumento speed
       
    }

    IEnumerator Attack()
    {
        
            //electricityVFX.GetComponent<VisualEffect>().SetFloat("Speed",maxSpeed);
        effectTrial.Play();
        yield return new WaitForSeconds( 0.5f);
        effectTrial.Stop();
       
        // electricityVFX.GetComponent<VisualEffect>().SetFloat("Speed", Mathf.Lerp(maxSpeed,startSpeed, 1f));


    }

    IEnumerator StopSpellMode()
    {
        yield return new WaitForSeconds(30f);
        spellMode = false;
        electricLoop.Stop();
        animator.SetLayerWeight(5, 0);
        animator.SetLayerWeight(0, 1);
        electricityVFX.SetActive(false);
       
    }
}
