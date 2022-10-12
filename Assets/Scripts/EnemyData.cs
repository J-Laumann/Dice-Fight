using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    DAMAGE,
    HIGHROLLER,
    FISHERMAN,
    BURNING,
    FINALBOSS
}

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Enemy Data", order = 1)]
public class EnemyData : ScriptableObject
{
    public string enemyID;
    public int health;
    public int attack;
    public EnemyType type;
}
