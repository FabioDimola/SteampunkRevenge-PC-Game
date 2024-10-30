using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DissolvingController : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMesh;
    private Material[] skinnedMaterials;
    public VisualEffect VFX_Graphs;
    public GameObject particles;
    public float dissolveRate = 0.0125f;
    public float refreshRate = 0.025f;

    private void Start()
    {
        particles.SetActive(false);
        if(skinnedMesh != null)
        {
            skinnedMaterials = skinnedMesh.sharedMaterials;

        }
        for (int i = 0; i < skinnedMaterials.Length; i++)
        {
            skinnedMaterials[i].SetFloat("_DissolveAmmount",0);
        }
    }



    private void Update()
    {
       
    }



    IEnumerator DissolveCo()
    {
        if(VFX_Graphs != null)
        {
            VFX_Graphs.Play();
        }
        particles.SetActive(true);
        if(skinnedMaterials.Length > 0)
        {
            float counter = 0;
            while (this.skinnedMaterials[0].GetFloat("_DissolveAmmount") < 1 ) // il nome corrisponed al #reference del graph settings
            {
                //decrease
                counter += dissolveRate;
                for(int i = 0; i < skinnedMaterials.Length; i++)
                {
                    this.skinnedMaterials[i].SetFloat("_DissolveAmmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }
}
