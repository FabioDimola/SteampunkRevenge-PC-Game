using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyManagerPooling : MonoBehaviour
{



    [SerializeField] private float spawnTime = 25f;
    [SerializeField] private float spawnGhostTime = 25f;
    private Coroutine _spawnCoroutine;
    private Coroutine _spawnGhostCoroutine;
    
    private void Start()
    {

        
        _spawnCoroutine = StartCoroutine(EnemyDrop());
        if(EnemyPooling.Instance.spawnPoint.Length > 0)
        _spawnGhostCoroutine = StartCoroutine(EnemyDrop());
        
    }

   

    IEnumerator EnemyDrop()
    {
        Debug.Log("Coroutine Started");


        while(true)
        {
            var enemy = EnemyPooling.Instance.GetZombie();
            enemy.gameObject.SetActive(true);
            if (spawnTime > 30)
            {
                spawnTime -= 1f;
            }
            yield return new WaitForSeconds(spawnTime);
        }


       

    }


}
