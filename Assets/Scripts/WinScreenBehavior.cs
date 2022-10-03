using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenBehavior : MonoBehaviour
{
    public GameObject winScreen;
    public string enemyID;

    private void Awake()
    {
        if (PlayerPrefs.GetInt(enemyID + "_DEAD", 0) == 1)
        {
            winScreen.SetActive(true);
            gameObject.GetComponent<PlayerController>().isPaused = true;
        }
    }
}
