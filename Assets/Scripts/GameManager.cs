using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool testing;
    public List<DieType> myDice;
    public List<AbilityData> playerAbilities;

    public Sprite[] diceSprites;

    public GameObject newDiePrefab, dieSlotPrefab, abilityPrefab;
    public Transform diceParent, abilitiesParent;

    public Image enemyHealthbar;
    public Image playerHealthbar;
    public static EnemyData enemy;
    public static string enemyID;
    public int enemyHp;

    [Header("TESTING VARIABLES")]
    public int currentHp;
    public EnemyData testEnemy;

    public static GameManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        if (enemy == null)
            enemy = testEnemy;

        enemyHp = enemy.health;

        if(currentHp == 0)
            currentHp = PlayerData.maxHp;

        if (!testing)
        {
            playerAbilities = PlayerData.pand.abilities;
            myDice = PlayerData.pand.dice;
        }

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
        PlayerPrefs.SetInt(enemyID + "_DEAD", 1);
        PlayerData.souls++;

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

        currentHp -= 13;

        playerHealthbar.fillAmount = (float)currentHp / (float) PlayerData.maxHp;

        SetupDice();
        SetupAbilities();
    }
}
