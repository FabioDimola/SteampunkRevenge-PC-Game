using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Objects/Enemy", fileName = "Enemy")]
public class EnemyScriptable : ScriptableObject
{
    public string name;
    public EnemyType type;
    public int maxLife;



}


public enum EnemyType
{
    Vedetta,
    Zombie,
    Robot
}