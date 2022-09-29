using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 Brian Sida
 Project 2
 Description: Manages tutorial UI
*/
public class TutorialUIManager : MonoBehaviour
{
    public Text nameText;
    public Text tutorialText;

    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void OverWorldTutorial(Tutorial tutorial)
    {
        // WIP: Add code to show dialogue box (animator?)

        nameText.text = tutorial.name;

        sentences.Clear();

        // looks through string array and adds news sentences to the queue
        foreach (string sentence in tutorial.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextTutorial();
    }

    public void BattleTutorial(Tutorial tutorial)
    {
        nameText.text = tutorial.name;

        sentences.Clear();

        // looks through string array and adds news sentences to the queue
        foreach (string sentence in tutorial.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextTutorial();
    }

    public void DisplayNextTutorial()
    {
        if (sentences.Count == 0)
        {
            EndTutorial();
            return;
        }

        // If queue isn't empty, print dequeue the next tutorial
        string tutorial = sentences.Dequeue();
        tutorialText.text = tutorial;
    }

    void EndTutorial()
    {
        // WIP: Add code to hide dialogue box
        Debug.Log("End of tutorial.");
    }
}
