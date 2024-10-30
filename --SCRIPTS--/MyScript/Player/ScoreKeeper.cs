using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private int _score;

    public int Score => _score; //rendiamo pubblica la get non la set


    public void AddToScore(int add)
    {
        _score +=  Mathf.Abs(add);
    }





}
