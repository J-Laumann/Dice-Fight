using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "New Ability", order = 1)]
public class AbilityData : ScriptableObject
{
    public string abilityID;
    public string abilityName, abilityDesc;
    public int uses = -1;

    public DieSlotData dieSlot;

    public AbilityData(AbilityData og)
    {
        abilityID = og.abilityID;
        abilityName = og.abilityName;
        abilityDesc = og.abilityDesc;
        uses = og.uses;
        dieSlot = og.dieSlot;
    }

}
