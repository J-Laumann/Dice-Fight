using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public Button continueButton, newGameButton;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Souls", 0) > 0)
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("OverWorld");
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        Continue();
    }
}
