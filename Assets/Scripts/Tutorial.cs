using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Brian Sida
 Project 2
 Description: Contains all information we need to know about tutorials
*/
[System.Serializable]
public class Tutorial
{
    public string name;

    [TextArea(3,10)]
    public string[] sentences;
}
