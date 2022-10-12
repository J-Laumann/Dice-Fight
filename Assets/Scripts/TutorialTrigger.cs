using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Brian Sida
 Project 2
 Description: Triggers tutorial when button is pressed
*/
public class TutorialTrigger : MonoBehaviour
{
    public Tutorial dialogue;

    private void Awake()
    {
        if (PlayerData.souls != 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void TriggerBattleTutorial()
    {
        // Checks if player is in their first fight in the game
        if (PlayerPrefs.GetInt("bTutorial", 0) == 0)
        {
            FindObjectOfType<TutorialUIManager>().BattleTutorial(dialogue);
            PlayerPrefs.SetInt("bTutorial", 1);
        }
    }

    public void TriggerOverworldTutorial()
    {
        if (PlayerPrefs.GetInt("oTutorial", 0) == 0)
        {
            FindObjectOfType<TutorialUIManager>().OverWorldTutorial(dialogue);
            PlayerPrefs.SetInt("oTutorial", 1);
        } 
    }
}
