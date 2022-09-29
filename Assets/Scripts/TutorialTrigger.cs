using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public Tutorial dialogue;

    public void TriggerTutorial()
    {
        // WIP
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
