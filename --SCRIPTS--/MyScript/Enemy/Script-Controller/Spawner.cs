using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnTime = 40f;
    public Enemy[] spawnableEnemies;
    public Transform[] spawnPoints;


    public Logger logger;

    public int Counter = 0;

   
   
    public bool isSpawning = false;

    private int enemiesActive;
    GameObject enemy;

    private bool startGame = true;
    private Coroutine _spawnCoroutine;

    private void Start()
    {

       _spawnCoroutine = StartCoroutine(EnemyDrop());
    }


    // Update is called once per frame
    void Update()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (enemy == null)
        {
            Counter = 0;
        }

       if(enemiesActive == 4)
        {
            StopCoroutine(_spawnCoroutine);

            if(Counter == 0)
            {
                enemiesActive = 0;
                StartCoroutine(EnemyDrop());
            }
        }
       
        
      
           


    }


  

    IEnumerator EnemyDrop()
    {
        startGame = true;

       
            while (enemiesActive < 4)
            {
                isSpawning = true;
                int randomEnemy = Random.Range(0, spawnableEnemies.Length);
                Counter++;
                Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(spawnableEnemies[randomEnemy].gameObject, randomPoint.position, randomPoint.rotation);

               
                yield return new WaitForSeconds(spawnTime);
                enemiesActive++;
            }
        
    }



}
