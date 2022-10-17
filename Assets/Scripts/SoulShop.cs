using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoulShop : MonoBehaviour
{

    public TMP_Text soulsText;

    public static SoulShop instance;

    GameObject soulShop;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        soulShop = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            soulShop.SetActive(!soulShop.activeSelf);

        soulsText.text = "Souls: " + PlayerData.souls;
    }

    public void Purchase(int cost, AbilityData ability, AbilityData oldAbility, GameObject button)
    {
        if (PlayerData.souls >= cost)
        {
            PlayerData.souls -= cost;
            PlayerData.pand.abilities.Add(ability);
            if(oldAbility != null)
                PlayerData.pand.abilities.Remove(oldAbility);
            Destroy(button.gameObject);
        }
    }

    public void Purchase(int cost, DieType die, GameObject button)
    {
        if (PlayerData.souls >= cost)
        {
            PlayerData.souls -= cost;
            PlayerData.pand.dice.Add(die);
            Destroy(button.gameObject);
        }
    }


}
