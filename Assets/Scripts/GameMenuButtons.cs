using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuButtons : MonoBehaviour
{
    public List<AbilityData> startingAbilities;
    public List<DieType> startingDice;

    public void Restart()
    {
        PlayerPrefs.DeleteAll();

        PlayerData.maxHp = PlayerPrefs.GetInt("maxHp", 20);
        PlayerData.souls = PlayerPrefs.GetInt("souls", 0);

        if (PlayerPrefs.HasKey("pand"))
            PlayerData.pand = JsonUtility.FromJson<PlayerAnD>(PlayerPrefs.GetString("pand"));
        else
            PlayerData.pand = new PlayerAnD(startingAbilities, startingDice);

        SceneManager.LoadScene("OverWorld");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
