using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerFunc : MonoBehaviour
{

    //UI
    public GameObject infoPanel;
    public TMP_Text gasphereTXT;
    public TMP_Text powerSphereTXT;
    public GameObject infoPanel3;

    private int infoRedCount;
    private int infoBluCount;
    
    public static int gasPhereCount=0;
    public static int powerSphereCount=0;


   // [SerializeField] private int maxEnergy = 10;
    //[SerializeField] private int minEnergy = 0;

    //Imposta barra energia
   // public Image energyBar;
    public Gradient gradient;
    public int currentEnergy;

   

    //TeleportField
    public CharacterController player;
    public GameObject teleportPosition;
    public Animator animator;
    public GameObject teleportAlert;




    


    // Start is called before the first frame update
    void Start()
    {
       
       //energyBar.fillAmount = 0;
       //energyBar.color = gradient.Evaluate(energyBar.fillAmount);

       currentEnergy = 0;

        gasphereTXT.SetText("0");
        powerSphereTXT.SetText("0");

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (gasPhereCount > 0)
            {
                StartCoroutine("TeleportFunc");
                animator.SetBool("isJumping", true);
                ScoreManager.instance.scoreC.gasPhere--; //quantita di energia necessaria rimasta (registrato su file salvataggio)
                gasPhereCount--; //energia consumata (posso toglierlo e lasciare come rif score manager?)
                Debug.Log("Gasphere in possesso" + gasPhereCount);
                gasphereTXT.SetText(gasPhereCount.ToString());
            }
            /* else
             {
                 Time.timeScale = 0;
                 teleportAlert.SetActive(true);//attiva pannello che avvisa che non si può effettuare il teletrasporto
             }
            */

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "BonusBlu" )//raccolta oggetti utili per il potenziamento
        {

            infoBluCount++;
            gasPhereCount++;
            if(infoBluCount ==1) //la prima volta che si entra in contatto viene mostrato msg spiegazione
            {
                Time.timeScale = 0; //stop del gioco
                infoPanel.SetActive(true); // info oggetto raccolto

            }
            //occesso variabile emissive intensity?

            gasphereTXT.SetText(gasPhereCount.ToString());
         

            //AddEnergy();
            ScoreManager.instance.scoreC.gasPhere += 1; //aggiunta punteggio file salvataggio
            Debug.Log(gasPhereCount);
            Destroy(other.gameObject); //oggetto distrutto in scena

           


        }
        if (other.gameObject.tag == "BonusRed")//raccolta oggetti utili per il potenziamento
        {

            infoRedCount++;
            powerSphereCount++;
           if (infoRedCount == 1) //la prima volta che si entra in contatto viene mostrato msg spiegazione
            {
                Time.timeScale = 0; //stop del gioco
                infoPanel3.SetActive(true); // info oggetto raccolto

            }
            //occesso variabile emissive intensity?


            powerSphereTXT.SetText(powerSphereCount.ToString());
            //AddEnergy();
            ScoreManager.instance.scoreC.powerSphere += 1; //aggiunta punteggio file salvataggio
            Debug.Log(gasPhereCount);
            Destroy(other.gameObject); //oggetto distrutto in scena

            if(other.gameObject.tag == "NextLevel")
            {

                Debug.Log("next");
                SceneManager.LoadScene("CityScene");
            }


        }


    }

    IEnumerator TeleportFunc()
    {


        if (player != null)
        {
            player.transform.position = teleportPosition.transform.position;
            yield return new WaitForSeconds(0.6f);
            animator.SetBool("Teleport", false);
        }

    }






}
