using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAbility : MonoBehaviour
{

    public AbilityData ability;

    public TMP_Text nameText, descText, usesText;
    public DieSlot dieSlot;

    public void Setup(AbilityData _ability)
    {
        ability = new AbilityData(_ability);
        GenerateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateUI()
    {
        dieSlot.ability = this;
        dieSlot.data = ability.dieSlot;

        nameText.text = ability.abilityName;
        descText.text = ability.abilityDesc;

        UpdateUses();
    }

    public void UpdateUses()
    {
        if (ability.uses == 1)
        {
            usesText.text = ability.uses + " Use";
        }
        else
        {
            usesText.text = ability.uses + " Uses";
        }
    }

    public void DoAbility(int value)
    {
        ability.uses--;
        UpdateUses();

        // Do the ability! All hardcoded!
        if(ability.abilityID == "REROLL")
        {
            GameManager.instance.GiveNewDie();
        }

        dieSlot.UpdateUI();
    }
}
