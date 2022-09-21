using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnD
{
    public List<AbilityData> abilities;
    public List<DieType> dice;

    public PlayerAnD(List<AbilityData> abilities, List<DieType> dice)
    {
        this.abilities = abilities;
        this.dice = dice;
    }
}

public class PlayerData
{
    public static int maxHp;
    public static int souls;

    public static PlayerAnD pand;

    public static void SaveData()
    {
        Debug.Log("SAVED");
        PlayerPrefs.SetInt("maxHp", maxHp);
        PlayerPrefs.SetInt("souls", souls);

        PlayerPrefs.SetString("pand", JsonUtility.ToJson(pand));
        Debug.Log(JsonUtility.ToJson(pand));
    }
}
