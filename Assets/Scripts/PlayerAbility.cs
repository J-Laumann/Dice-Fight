using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAbility : MonoBehaviour
{

    public AbilityData ability, baseAbility;
    public int uses;

    public TMP_Text nameText, descText, usesText;

    public Transform dieSlotParent;
    public GameObject dieSlotPrefab;
    public List<DieSlot> dieSlots;

    public void Setup(AbilityData _ability)
    {
        dieSlots = new List<DieSlot>();
        baseAbility = _ability;
        ability = new AbilityData(_ability);
        for(int i = 0; i < ability.dieSlots.Length; i++)
        {
            ability.dieSlots[i] = new DieSlotData(ability.dieSlots[i]);
        }
        uses = _ability.uses;
        GenerateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateUI()
    {
        for(int i = 0; i < ability.dieSlots.Length; i++)
        {
            GameObject newSlot = Instantiate(dieSlotPrefab, dieSlotParent);
            DieSlot slot = newSlot.GetComponent<DieSlot>();
            dieSlots.Add(slot);
            slot.ability = this;
            slot.data = ability.dieSlots[i];
        }

        nameText.text = ability.abilityName;
        descText.text = ability.abilityDesc;

        UpdateUses();
    }

    public void Refresh()
    {
        uses = baseAbility.uses;
        foreach (Transform child in dieSlotParent)
            Destroy(child.gameObject);
        dieSlots = new List<DieSlot>();
        GenerateUI();
    }

    public void UpdateUses()
    {
        if(uses == -1)
        {
            usesText.text = "Infinite Uses";
        }
        else if (uses == 1)
        {
            usesText.text = uses + " Use";
        }
        else
        {
            usesText.text = uses + " Uses";
        }
    }

    public void DoAbility()
    {
        if(uses > 0)
            uses--;
        UpdateUses();


        // HERE HERE HERE HERE HERE 
        // MAKE ALL YOUR ABILITIES IN HERE!!!
        // Simply create a new else if statement to check your abilityID and then make it do stuff
        // feel free to copy paste

        // Simply rerolls the type of dice put into it
        if(ability.abilityID == "REROLL")
        {
            GameManager.instance.GiveNewDie(dieSlots[0].die.type);
        }

        // Deals damage equal to the dice
        else if(ability.abilityID == "ATTACK")
        {
            GameManager.instance.AttackEnemy(dieSlots[0].die.value);
        }

        // Deals damage equal to the dice in the two slots multiplied
        else if(ability.abilityID == "ATTACK_MULT")
        {
            GameManager.instance.AttackEnemy(dieSlots[0].die.value * dieSlots[1].die.value);
        }

        // If you fill the slot with a total of 8, it deals 10 damage
        else if(ability.abilityID == "ATTACK_FILL8")
        {
            GameManager.instance.AttackEnemy(10);
        }

        // If you fill the slot with a total of 20, it deals 25 damage
        else if (ability.abilityID == "ATTACK_FILL20")
        {
            GameManager.instance.AttackEnemy(25);
        }

        // Heals half of the dice value
        else if(ability.abilityID == "HEAL_DIV2")
        {
            GameManager.instance.TakeDamage(-dieSlots[0].die.value / 2);
        }
        // Heals for dice value
        else if(ability.abilityID == "HEAL")
        {
            GameManager.instance.TakeDamage(-dieSlots[0].die.value);
        }


        // This happens after, just resets all of the slots
        for(int i = 0; i < dieSlots.Count; i++)
        {
            DieSlot slot = dieSlots[i];

            if (slot.die)
                Destroy(slot.die.gameObject);

            slot.data.fillAmount = baseAbility.dieSlots[i].fillAmount;

            slot.UpdateUI();
        }
    }
}
