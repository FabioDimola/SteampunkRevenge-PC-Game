using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityPulledPlayer : MonoBehaviour
{
    [SerializeField] private Transform firePoint;


    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha2))
        {
            var bullet = UnityBulletManager.Instance.GetBullet();
            Vector3 position = new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z);
            bullet.transform.position = transform.position;

        }
    }
}
