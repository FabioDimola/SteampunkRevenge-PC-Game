using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ragdoll;
using UnityEngine.AI;

public class DollAttack : MonoBehaviour
{
    RagdollEnabler enable;
    private NavMeshAgent agent;
    [HideInInspector] public static bool bang = false;
    public GameObject fire;
    private void Start()
    {
        enable = GetComponentInChildren<RagdollEnabler>();
        agent = GetComponentInChildren<NavMeshAgent>();
    }


    private void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Punch")
        {
            fire.SetActive(true);
            fire.transform.localEulerAngles = transform.localEulerAngles;
            enable.EnableRagdoll();
            bang = true;
            foreach (Rigidbody rb in enable.rigidBodies)
            {
                rb.AddForce(new Vector3(2f, 5f, -230f));
                rb.velocity = Vector3.zero;
              
                //agent.enabled = false;
                
            }
            Destroy(this.gameObject, 8f);
            agent.isStopped = true;
        }
        if (other.gameObject.tag == "Sword")
        {
            enable.EnableRagdoll();
            foreach (Rigidbody rb in enable.rigidBodies)
            {
                rb.AddForce(new Vector3(2f, 5f, -230f));
                rb.velocity = Vector3.zero;

                //agent.enabled = false;
                Destroy(this.gameObject, 8f);
                agent.isStopped = true;
            }

        }
    }

    public void  ExplosionDoll()
    {

        enable.EnableRagdoll();
        foreach(Rigidbody rb in enable.rigidBodies)
        {
                rb.AddForce(new Vector3(2f,5f,-23f));
        }
        
        Debug.Log("Collision Detected");
    }



  
}
