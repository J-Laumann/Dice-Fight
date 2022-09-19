﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerData
{
    public int currentHp = 20;
    public int maxHp = 20;
}


public class GameManager : MonoBehaviour
{

    public DieType[] myDice;
    public AbilityData[] playerAbilities;

    public Sprite[] diceSprites;

    public GameObject newDiePrefab, dieSlotPrefab, abilityPrefab;
    public Transform diceParent, abilitiesParent;

    public Image enemyHealthbar;
    public EnemyData testEnemy;
    public static EnemyData enemy;
    public int enemyHp;
    public static PlayerData playerData;

    public static GameManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        if (enemy == null)
            enemy = testEnemy;

        enemyHp = enemy.health;

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
        foreach (DieType die in myDice)
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
        enemyHp -= damage;
        enemyHealthbar.fillAmount = (float)enemyHp / (float)enemy.health;

        if(enemyHp <= 0)
        {
            StartCoroutine(WinBattle());
        }
    }

    IEnumerator WinBattle()
    {
        TransitionHandler.instance.SlideIn();

        yield return new WaitForSeconds(1.5f);

        TransitionHandler.instance.SlideOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene("OverWorld");
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

    public IEnumerator DiceCountCheck()
    {
        yield return new WaitForEndOfFrame();
        if (diceParent.childCount == 0)
        {
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        foreach(Transform child in diceParent)
        {
            Destroy(child.gameObject);
        }

        foreach(Transform child in abilitiesParent)
        {
            Destroy(child.gameObject);
        }

        yield return new WaitForSeconds(0.5f);

        playerData.currentHp -= 13;
        print("Player health: " + playerData.currentHp);

        SetupDice();
        SetupAbilities();
    }
}
