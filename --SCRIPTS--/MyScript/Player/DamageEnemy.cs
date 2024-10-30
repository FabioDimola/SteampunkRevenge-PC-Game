using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    private EnemyHealth enemyLife;



    private void OnTriggerEnter(Collider other)
    {
        ///enemyLife = other.GetComponent<EnemyHealth>();

        if(other.gameObject.tag == "Vedetta")
        {
            enemyLife = other.GetComponent<EnemyHealth>();
            if(enemyLife.currentHealth > 0)
            {
                enemyLife.currentHealth -= 10;
                enemyLife.healthBar.UpdateHealthBar(100,enemyLife.currentHealth);
            }
        }
    }


}
