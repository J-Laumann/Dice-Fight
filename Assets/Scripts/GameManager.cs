using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public DieType[] myDice;
    public AbilityData[] playerAbilities;

    public Sprite[] diceSprites;

    public GameObject newDiePrefab, dieSlotPrefab, abilityPrefab;
    public Transform diceParent, abilitiesParent;

    public EnemyData testEnemy;
    public static EnemyData enemy;

    public static GameManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        if (enemy == null)
            enemy = testEnemy;

        SetupUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupUI()
    {
        SetupAbilities();
        SetupDice();
    }

    public void SetupDice()
    {
        foreach(DieType die in myDice)
        {
            GiveNewDie(die);
        }
    }

    public void GiveNewDie(DieType type)
    {
        GameObject newDie = Instantiate(newDiePrefab, diceParent);
        newDie.GetComponentInChildren<Die>().type = type;
    }

    public void AttackEnemyTest(int damage)
    {
        enemy.health -= damage;
        print("Damage: " + damage + "; Enemy health remaining: " + enemy.health);

        if(enemy.health <= 0)
        {
            print("Enemy Defeated!");
        }
    }

    public void SetupAbilities()
    {
        foreach (AbilityData playerAbility in playerAbilities)
        {
            GameObject newAbility = Instantiate(abilityPrefab, abilitiesParent);

            PlayerAbility newPA = newAbility.GetComponent<PlayerAbility>();

            AbilityData tempData = playerAbility;
            newPA.Setup(tempData);
        }
    }
}
