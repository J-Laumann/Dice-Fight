using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool testing;
    public List<DieType> myDice;
    List<AbilityData> myAbilities;
    public List<AbilityData> baseAbilities;

    public Sprite[] diceSprites;

    public GameObject newDiePrefab, dieSlotPrefab, abilityPrefab, burnEffect;
    public Transform diceParent, abilitiesParent;

    public Button endTurnButton;
    public Image enemyHealthbar;
    public Image playerHealthbar;
    public static EnemyData enemy;
    public static string enemyID;
    public int enemyHp;
    private AudioSource[] aud;

    [Header("Sounds")]
    public AudioClip singleDie;
    public AudioClip multipleDice;
    public AudioClip victory;
    public AudioClip defeat;
    public AudioClip[] attackSounds;
    public AudioClip[] highRollerSounds;
    public AudioClip fisherSound;
    public AudioClip pyroSound;

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

        aud = GetComponents<AudioSource>();

        currentHp = PlayerData.maxHp;

        if (!testing)
        {
            baseAbilities = PlayerData.pand.abilities;
            myDice = PlayerData.pand.dice;
        }

        myAbilities = new List<AbilityData>();
        for (int i = 0; i < baseAbilities.Count; i++)
        {
            myAbilities.Add(new AbilityData(baseAbilities[i]));
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
        aud[0].PlayOneShot(multipleDice, 1.0f);
        foreach (DieType die in myDice)
        {
            GiveNewDie(die);
        }
    }

    public void GiveNewDie(DieType type)
    {
        aud[0].PlayOneShot(singleDie, 1.0f);
        GameObject newDie = Instantiate(newDiePrefab, diceParent);
        newDie.GetComponentInChildren<Die>().type = type;
    }

    public void AttackEnemy(int damage)
    {
        aud[1].PlayOneShot(attackSounds[Random.Range(0, attackSounds.Length)], 1.0f);
        DealDamage(damage);

        if(enemyHp <= 0)
        {
            StartCoroutine(WinBattle());
        }
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        playerHealthbar.fillAmount = (float)currentHp / (float)PlayerData.maxHp;
    }

    public void DealDamage(int damage)
    {
        enemyHp -= damage;
        enemyHealthbar.fillAmount = (float)enemyHp / (float)enemy.health;
    }

    IEnumerator WinBattle()
    {
        aud[2].Stop();
        yield return new WaitForSeconds(0.2f);
        aud[0].PlayOneShot(victory, 1.0f);

        yield return new WaitForSeconds(0.7f);

        TransitionHandler.instance.SlideIn();
        PlayerPrefs.SetInt(enemyID + "_DEAD", 1);
        PlayerData.souls++;

        if (enemyID.Contains("Door"))
            PlayerData.maxHp += 5;

        yield return new WaitForSeconds(1.5f);

        TransitionHandler.instance.SlideOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene("OverWorld");
    }

    public void SetupAbilities()
    {
        foreach (AbilityData playerAbility in myAbilities)
        {
            GameObject newAbility = Instantiate(abilityPrefab, abilitiesParent);

            PlayerAbility newPA = newAbility.GetComponent<PlayerAbility>();

            AbilityData tempData = playerAbility;
            newPA.Setup(tempData);
        }
    }

    public void RefreshAbilities()
    {
        abilitiesParent.gameObject.SetActive(true);
        foreach (Transform child in abilitiesParent)
        {
            PlayerAbility ability = child.GetComponent<PlayerAbility>();
            ability.Refresh();
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

    public void EndTurn()
    {
        if (enemyHp > 0)
        {
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        foreach(Transform child in diceParent)
        {
            if (child.GetComponent<DieSlot>().die)
                child.GetComponent<DieSlot>().die.killing = true;
            Destroy(child.gameObject);
        }

        abilitiesParent.gameObject.SetActive(false);

        endTurnButton.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        aud[1].PlayOneShot(attackSounds[Random.Range(0, attackSounds.Length)], 1.0f);

        TakeDamage(enemy.attack);

        yield return new WaitForSeconds(1f);

        if (currentHp <= 0)
        {
            aud[2].Stop();
            yield return new WaitForSeconds(0.2f);
            aud[0].PlayOneShot(defeat, 1.0f);

            yield return new WaitForSeconds(0.7f);

            TransitionHandler.instance.SlideIn();

            yield return new WaitForSeconds(1.5f);

            TransitionHandler.instance.SlideOut();
            UnityEngine.SceneManagement.SceneManager.LoadScene("OverWorld");
        }

        SetupDice();
        RefreshAbilities();
        endTurnButton.gameObject.SetActive(true);

        if (enemy.type == EnemyType.BURNING)
        {
            StartCoroutine(BurnRandomDie());
        }
        else if(enemy.type == EnemyType.HIGHROLLER)
        {
            StartCoroutine(HighRollerEffect());
        }
        else if(enemy.type == EnemyType.FISHERMAN)
        {
            StartCoroutine(FishermanEffect());
        }
        else if(enemy.type == EnemyType.FINALBOSS)
        {
            StartCoroutine(BurnRandomDie());
            StartCoroutine(FishermanEffect());
        }
    }

    public IEnumerator HighRollerEffect()
    {
        yield return new WaitForSeconds(1f);
        // Yeehaw here
        Debug.LogError("YEEHAW!!");
        aud[1].PlayOneShot(highRollerSounds[Random.Range(0, highRollerSounds.Length)], 1.0f);

        int highNumb = 0;
        Die highDie = null;
        foreach(Transform child in diceParent)
        {
            Die die = child.GetComponent<DieSlot>().die;
            if(die.value >= highNumb)
            {
                highNumb = die.value;
                highDie = die;
            }
        }

        yield return new WaitForSeconds(1f);
        aud[0].PlayOneShot(singleDie, 1f);
        highDie.StartCoroutine(highDie.RollDie());
    }

    public IEnumerator FishermanEffect()
    {
        yield return new WaitForSeconds(0.5f);
        aud[1].PlayOneShot(fisherSound, 1.0f);
        yield return new WaitForSeconds(0.2f);
        int rand = Random.Range(0, diceParent.childCount - 1);
        GameObject chosenDie = diceParent.GetChild(rand).gameObject;
        float speed = 1000;
        while (chosenDie.transform.position.y < 1000)
        {
            chosenDie.transform.Translate(Vector3.up * Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
        Destroy(chosenDie);
    }

    public IEnumerator BurnRandomDie()
    {
        yield return new WaitForEndOfFrame();
        aud[1].PlayOneShot(pyroSound, 1.0f);
        int rand = Random.Range(0, diceParent.childCount - 1);
        GameObject chosenDie = diceParent.GetChild(rand).gameObject;
        chosenDie.GetComponent<DieSlot>().die.SetBurning();
    }
}
