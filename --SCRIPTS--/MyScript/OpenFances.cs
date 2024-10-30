using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFances : MonoBehaviour
{
    public Animator animator;


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("Open", true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("Open", false);

        }

    }
}
