using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public static int currentHp, maxHp;
    public static int souls;

    public static void SaveData()
    {
        PlayerPrefs.SetInt("currentHp", currentHp);
        PlayerPrefs.SetInt("maxHp", maxHp);
        PlayerPrefs.SetInt("souls", souls);
    }
}
