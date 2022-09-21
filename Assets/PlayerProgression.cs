using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerUnlock
{
    public int soulCost;
    public AbilityData ability;
    public DieType die;
}

public class PlayerProgression : MonoBehaviour
{

    public List<PlayerUnlock> unlocks;

    // Start is called before the first frame update
    void Start()
    {
        foreach(PlayerUnlock unlock in unlocks)
        {
            if(PlayerData.souls >= unlock.soulCost)
            {
                if(PlayerPrefs.GetInt("unlock"+unlock.soulCost, 0) == 0)
                {
                    if(unlock.ability)
                        PlayerData.pand.abilities.Add(unlock.ability);

                    if(unlock.die != DieType.NULL)
                        PlayerData.pand.dice.Add(unlock.die);

                    PlayerPrefs.SetInt("unlock" + unlock.soulCost, 1);
                }
            }
        }
    }
}
