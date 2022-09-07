using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int amountOfDice;
    public AbilityData[] playerAbilities;

    public Sprite[] diceSprites;

    public GameObject newDiePrefab, dieSlotPrefab, abilityPrefab;
    public Transform diceParent, abilitiesParent;

    public static GameManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

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
        for(int i = 0; i < amountOfDice; i++)
        {
            GiveNewDie();
        }
    }

    public void GiveNewDie()
    {
        Instantiate(newDiePrefab, diceParent);
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
