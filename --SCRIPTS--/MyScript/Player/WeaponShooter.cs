using PlayerController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponShooter : MonoBehaviour
{
    
    

    [SerializeField] private Transform firePoint;
    



    private void Update()
    {
        //  ChangeAnimatorState();
        if (Input.GetMouseButtonDown(0))
        {
            //anim.SetTrigger("Shoot");

            var bullet = UnityBulletManager.Instance.GetBullet();
            bullet.transform.position = firePoint.position;
        }
    }




   
}
