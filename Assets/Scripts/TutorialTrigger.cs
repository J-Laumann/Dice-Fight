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
        if (!EnterBattleScript.firstFight && PlayerData.souls == 0)
        {
            FindObjectOfType<TutorialUIManager>().BattleTutorial(dialogue);
        }
        else
        {
            FindObjectOfType<TutorialUIManager>().OverWorldTutorial(dialogue);
        } 
    }
}
