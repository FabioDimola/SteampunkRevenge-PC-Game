using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class UnityZombieManager : MonoBehaviour
{
    [SerializeField] private UnityPulledZombie pulledZombiePrefab;

    private ObjectPool<UnityPulledZombie> pool;
    [SerializeField] private Transform spawnPoint;
    private float currentLifeTime;
   
    private float maxLifetime = 20f;
   
    public static UnityZombieManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        pool = new ObjectPool<UnityPulledZombie>(AddZombie, t => t.gameObject.SetActive(true), t => t.gameObject.SetActive(false)); //t valore generico e gli associamo il valore di tipo UnityPulledBullet
        //va a riprendere in t l'oggetto definito nella func precedente (AddBullet)
    }

    private void OnEnable()
    {
        currentLifeTime = maxLifetime;
    }

    private void Update()
    {
         currentLifeTime -= Time.deltaTime;
        if (currentLifeTime <= 0)
        {
            var zombieSpawn = GetZombie();
            zombieSpawn.transform.position = spawnPoint.position;
            currentLifeTime = maxLifetime;
        }
           
        

      
    }

   


    IEnumerator SpawnZombie()
    {
        WaitForSeconds wait = new WaitForSeconds(30);
        yield return wait;
        var zombieSpawn = GetZombie();
        zombieSpawn.transform.position = transform.position;
        
    }



    private UnityPulledZombie AddZombie()
    {
        //istanziamo i bullet
        var zombie = Instantiate(pulledZombiePrefab, spawnPoint.position,  spawnPoint.rotation,transform);
        zombie.SetPool(pool);
       
        return zombie;
    }


    public UnityPulledZombie GetZombie()
    {
       
        return pool.Get();
    }
}
