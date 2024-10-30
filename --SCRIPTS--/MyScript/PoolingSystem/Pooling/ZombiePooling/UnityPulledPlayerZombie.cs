using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityPulledPlayerZombie : MonoBehaviour
{
   


    private void Update()
    {
        StartCoroutine(SpawnZombie());
    }



    IEnumerator SpawnZombie()
    {
        WaitForSeconds wait = new WaitForSeconds(30);
        var zombie = UnityZombieManager.Instance.GetZombie();
        yield return wait;
    }



}
