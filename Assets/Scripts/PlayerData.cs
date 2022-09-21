using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public static int maxHp;
    public static int souls;

    public static void SaveData()
    {
        PlayerPrefs.SetInt("maxHp", maxHp);
        PlayerPrefs.SetInt("souls", souls);
    }
}
