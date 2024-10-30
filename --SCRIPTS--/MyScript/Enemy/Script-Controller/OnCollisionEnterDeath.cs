using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionEnterDeath : MonoBehaviour
{

    public string[] targetTag;
    public EnemyController enemy;

    private int countAttack = 0;
    public bool hitByWeapon = false;

    

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < targetTag.Length; i++)
        {
            if (collision.gameObject.tag == targetTag[i])
            {
           
                    enemy.Dead(collision.contacts[0].point);
                    
            


               
            }
        }
        
    }
}
