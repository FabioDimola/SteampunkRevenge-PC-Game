using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSistem : MonoBehaviour
{
    public AudioSource walkStep;
    public AudioSource jumpStep;
    public AudioSource landStep;
    public AudioSource swordSound;
    public AudioSource punchSound;
    public AudioSource electricSound;
    public AudioSource bloodSound;
    public AudioSource teleportSound;
    public void WalkSound()
    {
        walkStep.Play();

    }

    public void JumpStep()
    {
        jumpStep.Play();
    }

    public void LandSound()
    {
        landStep.Play();
    }

    public void SwordSound()
    {
        swordSound.Play();
    }

    public void PunchSound()
    {
        punchSound.Play();
    }

    public void ElectricSound()
    {
        electricSound.Play();
    }

    public void BloodSound()
    {
        bloodSound.Play();
    }

    public void TeleportSound()
    {
        teleportSound.Play();   
    }
}
