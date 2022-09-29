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

    public void TriggerTutorial()
    {
        // WIP: Possibly create UI for battle scene 
        if (PlayerData.souls != 0)
        {
            FindObjectOfType<TutorialUIManager>().OverWorldTutorial(dialogue);
        }
        else
        {
            FindObjectOfType<TutorialUIManager>().BattleTutorial(dialogue);
        }
    }
}
