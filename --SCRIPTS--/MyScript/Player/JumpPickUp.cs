using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;

public class JumpPickUp : PickUpObject
{
    public override void PickMeUp(PickUpInteractor interactor)
    {
       if(interactor.TryGetComponent<PlayerMover>(out PlayerMover mover)) //se quello che mi è salito sopra è un player mover mi fa ssaltare
        {
            mover.Jump();
        }
    }

   
}
