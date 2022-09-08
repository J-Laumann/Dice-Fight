using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Brian Sida
 * Project 1
 * Description: Camera follows player
*/
public class CamFollowPlayer : MonoBehaviour
{
    // drag the player onto this reference in the Inspector
    public GameObject player;

    public Vector3 offset = new Vector3(0, 4, -8);

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
