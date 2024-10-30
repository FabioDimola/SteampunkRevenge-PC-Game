using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VedettaController : MonoBehaviour
{
    private Animator anim;
    public GameObject healthBar;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        healthBar.SetActive(false);
    }



    private void OnTriggerEnter(Collider other)
    {
      

        if(other.gameObject.tag == "Player")
        {
            healthBar.SetActive(true);
        }
        if(other.gameObject.tag == "Projectile")
        {
            healthBar.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            healthBar.SetActive(false);
        }
    }
}
