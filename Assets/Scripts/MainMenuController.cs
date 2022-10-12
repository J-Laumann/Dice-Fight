using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public Button continueButton, newGameButton;

    public List<AbilityData> startingAbilities;
    public List<DieType> startingDice;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("souls"))
        {
            continueButton.interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        PlayerData.maxHp = PlayerPrefs.GetInt("maxHp", 20);
        PlayerData.souls = PlayerPrefs.GetInt("souls", 0);

        PlayerPrefs.SetInt("oTutorial", 0);
        PlayerPrefs.SetInt("bTutorial", 0);

        if (PlayerPrefs.HasKey("pand"))
            PlayerData.pand = JsonUtility.FromJson<PlayerAnD>(PlayerPrefs.GetString("pand"));
        else
            PlayerData.pand = new PlayerAnD(startingAbilities, startingDice);

        UnityEngine.SceneManagement.SceneManager.LoadScene("OverWorld");
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        Continue();
    }
}
