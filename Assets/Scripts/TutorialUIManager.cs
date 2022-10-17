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

    private void Awake()
    {
        sentences = new Queue<string>();
    }

    public void OverWorldTutorial(Tutorial tutorial)
    {
        // Shows dialogue box
        TransitionHandler.instance.DialogueOpen();
        Debug.Log("Dialogue Open.");

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
        TransitionHandler.instance.DialogueOpen();

        nameText.text = tutorial.name;

        sentences.Clear();

        // looks through string array and adds news sentences to the queue
        foreach (string sentence in tutorial.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextTutorial();
    }

    public void ShopTutorial(Tutorial tutorial)
    {


        TransitionHandler.instance.DialogueOpen();

        nameText.text = tutorial.name;

        sentences.Clear();

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

        // If queue isn't empty, print the next sentence
        string tutorial = sentences.Dequeue();
        tutorialText.text = tutorial;
    }

    void EndTutorial()
    {
        TransitionHandler.instance.DialogueClose();
        Debug.Log("End of tutorial.");
    }
}
