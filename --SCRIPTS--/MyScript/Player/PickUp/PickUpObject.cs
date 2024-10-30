using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;

public  class PickUpObject : MonoBehaviour
{

    public virtual void PickMeUp(PickUpInteractor interactor)
    {
        
    }
    

    protected bool isInteractorPlayer(PickUpInteractor interactor)
    {
        return interactor.TryGetComponent<PlayerMover>(out PlayerMover mover);
    }
}
