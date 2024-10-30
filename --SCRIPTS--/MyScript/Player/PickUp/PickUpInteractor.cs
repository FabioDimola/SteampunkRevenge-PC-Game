using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpInteractor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<PickUpObject>(out PickUpObject pickUpObject))
        {
            pickUpObject.PickMeUp(this);
        }
    }
}
