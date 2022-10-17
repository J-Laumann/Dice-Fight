using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyImage
{
    public EnemyType type;
    public Sprite sprite;
}

public enum EnemyType
{
    DAMAGE,
    HIGHROLLER,
    FISHERMAN,
    BURNING,
    EGIRL,
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
