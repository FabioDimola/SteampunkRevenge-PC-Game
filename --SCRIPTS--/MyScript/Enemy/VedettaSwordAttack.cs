using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VedettaSwordAttack : MonoBehaviour
{

    public Animator anim;

    

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "Sword")
        {
            anim.SetTrigger("Hit");
        }
    }
}
