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

    public void TriggerTutorial()
    {
        // Checks if player is in their first fight in the game
        if (PlayerPrefs.GetInt("bTutorial") == 0)
        {
            FindObjectOfType<TutorialUIManager>().BattleTutorial(dialogue);
            PlayerPrefs.SetInt("bTutorial", 1);
        }
        
        if (PlayerPrefs.GetInt("oTutorial") == 0)
        {
            FindObjectOfType<TutorialUIManager>().OverWorldTutorial(dialogue);
            PlayerPrefs.SetInt("oTutorial", 1);
        } 
    }
}
