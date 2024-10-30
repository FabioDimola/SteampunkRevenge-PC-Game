using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{

    [SerializeField] private Zombie[] pulledZombiePrefab;
    //[SerializeField] private Enemy[] pulledGhostPrefab;
    private Queue<Zombie> zombiePool = new Queue<Zombie>();
    //private Queue<Enemy> ghostPool = new Queue<Enemy>();
    public Transform[] spawnPoint;

    //public Transform[] spawnGhostPoint;
    public static EnemyPooling Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

    }

    
   

    private void IncreasePoolZombieSize(int count) //count grandezza pool
    {
        for (int i = 0; i < count; i++)
        {

            int randomEnemy = Random.Range(0, pulledZombiePrefab.Length);
            int randomPos = Random.Range(0, spawnPoint.Length);
            var zombie = Instantiate(pulledZombiePrefab[randomEnemy], spawnPoint[randomPos].transform);//instanzia enemy nel parent a cui è collegato lo script
            zombie.gameObject.SetActive(false); //deve essere disattivato inizialmente
            zombiePool.Enqueue(zombie); //inserimento in coda del enemy creato

        }

    }

    private void Start()
    {

       
        IncreasePoolZombieSize(30);

    }


    public Zombie GetZombie()
    {
        if (zombiePool.Count == 0)
            IncreasePoolZombieSize(30);
        return zombiePool.Dequeue();
    }


    public void ReturnZombieToPool(Zombie zombie)
    {
        zombie.gameObject.SetActive(false); //disattivo proiettile prima di farlo rientrare nella coda
        zombiePool.Enqueue(zombie); //ritorno in coda del bullet

    }

   


}
