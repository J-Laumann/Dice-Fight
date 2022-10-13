using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "New Ability", order = 1)]
public class AbilityData : ScriptableObject
{
    public string abilityID;
    public string abilityName, abilityDesc;
    public int uses = -1;

    public DieSlotData[] dieSlots;

    public AbilityData(AbilityData og)
    {
        abilityID = og.abilityID;
        abilityName = og.abilityName;
        abilityDesc = og.abilityDesc;
        uses = og.uses;

        dieSlots = new DieSlotData[og.dieSlots.Length];
        for(int i = 0; i < og.dieSlots.Length; i++)
        {
            dieSlots[i] = (DieSlotData)og.dieSlots[i].Clone();
        }
    }

}
