using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VedettaSound : MonoBehaviour
{
    public AudioSource step;
    public AudioSource fire;
    public AudioSource hit;


    public void PlayStep()
    {
        step.Play();
    }

    public void PlayShoot()
    {
        fire.Play();
    }

    public void PlayHit()
    {
        hit.Play();
    }
}
