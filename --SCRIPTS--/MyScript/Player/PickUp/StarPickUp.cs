using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;

public class StarPickUp : PickUpObject
{
    public override void PickMeUp(PickUpInteractor interactor)
    {


        if (isInteractorPlayer(interactor)) //se quello che mi è salito sopra è un player mover mi fa ssaltare
        {
            Debug.Log("Player got a star");

            interactor.GetComponent<ScoreKeeper>().AddToScore(1);

            Destroy(gameObject);
        }
        
    }
}
