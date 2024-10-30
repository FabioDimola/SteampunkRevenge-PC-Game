using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class UnityBulletManager : MonoBehaviour
{
    [SerializeField] private UnityPulledBullet pulledBulletPrefab;

    private ObjectPool<UnityPulledBullet> pool;
    [SerializeField] private Transform shootingPoint;
    public static UnityBulletManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        pool = new ObjectPool<UnityPulledBullet>(AddBullet, t => t.gameObject.SetActive(true), t => t.gameObject.SetActive(false)); //t valore generico e gli associamo il valore di tipo UnityPulledBullet
        //va a riprendere in t l'oggetto definito nella func precedente (AddBullet)
    }

    private UnityPulledBullet AddBullet()
    {
        //istanziamo i bullet
        var bullet = Instantiate(pulledBulletPrefab, shootingPoint.position, shootingPoint.rotation);
        bullet.SetPool(pool);
       
        return bullet;
    }


    public UnityPulledBullet GetBullet()
    {
        return pool.Get();
    }
}
