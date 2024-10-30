using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    private Animator anim;
    public Rigidbody bullet;
    public GameObject mirino;

    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("Rotation", true);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Rigidbody bulletClone;
            bulletClone=Instantiate(bullet, mirino.transform.position, mirino.transform.rotation);
            bulletClone.velocity = mirino.transform.TransformDirection(Vector3.forward * 10);
        }
    }
}
