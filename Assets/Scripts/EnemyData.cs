using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Enemy Data", order = 1)]
public class EnemyData : ScriptableObject
{
    public string enemyID;
    public int health;
    public int attack;
}
