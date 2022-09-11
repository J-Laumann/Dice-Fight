using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Enemy Data", order = 1)]
public class EnemyData : ScriptableObject
{
    [SerializeField] private int baseHealth = 7;
    public int health;
    public int attack;

    private void OnEnable()
    {
        health = baseHealth;
    }
}
