using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class UnityPulledBullet : MonoBehaviour
{


    [SerializeField] private float speed = 7f;
    [SerializeField] private float maxLifeTime=2f;
    public Transform shootingPoint;
    Rigidbody rb;
    private float currentLifeTime;
    private ObjectPool<UnityPulledBullet> pool;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        currentLifeTime = maxLifeTime;
    }

    private void Update()
    {
        rb.velocity = shootingPoint.forward * speed;
        
        currentLifeTime -= Time.deltaTime;
        if (currentLifeTime <= 0)
        {
            pool.Release(this);
        }
    }

    public void SetPool(ObjectPool<UnityPulledBullet> pool)
    {
        this.pool = pool;
    }
}
