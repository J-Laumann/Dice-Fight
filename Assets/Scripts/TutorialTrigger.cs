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

    public Tutorial shopDialogue;

    public Tutorial hiRoller;
    public Tutorial fisher;
    public Tutorial pyro;

    private void Start()
    {
        if (PlayerData.souls != 0)
        {
            gameObject.SetActive(false);
        }

        if (PlayerData.souls != 0 && PlayerPrefs.GetInt("sTutorial", 0) == 0)
        {
            FindObjectOfType<TutorialUIManager>().ShopTutorial(shopDialogue);
            PlayerPrefs.SetInt("sTutorial", 1);
        }

        if (GameManager.instance.GetEnemy() == EnemyType.HIGHROLLER && PlayerPrefs.GetInt("rollTutorial", 0) == 0)
        {
            FindObjectOfType<TutorialUIManager>().ShopTutorial(hiRoller);
            PlayerPrefs.SetInt("rollTutorial", 1);
        }

        if (GameManager.instance.GetEnemy() == EnemyType.FISHERMAN && PlayerPrefs.GetInt("fishTutorial", 0) == 0)
        {
            FindObjectOfType<TutorialUIManager>().ShopTutorial(fisher);
            PlayerPrefs.SetInt("fishTutorial", 1);
        }

        if (GameManager.instance.GetEnemy() == EnemyType.BURNING && PlayerPrefs.GetInt("pyroTutorial", 0) == 0)
        {
            FindObjectOfType<TutorialUIManager>().ShopTutorial(pyro);
            PlayerPrefs.SetInt("pyroTutorial", 1);
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
