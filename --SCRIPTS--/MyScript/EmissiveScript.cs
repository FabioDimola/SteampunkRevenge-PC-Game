using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissiveScript : MonoBehaviour
{
    // Start is called before the first frame update

    float emissiveIntensity = 200f;
    void Start()
    {
        //GetComponent<Renderer>().material.SetColor("_EmmissionColor", GetComponent<Renderer>().material.color * emissiveIntensity);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.SetColor("_EmmissionColor", GetComponent<Renderer>().material.color * emissiveIntensity);
    }
}
