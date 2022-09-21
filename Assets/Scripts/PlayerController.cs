using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Brian Sida
 * Project 1
 * Description: Controls player movement
*/
public class PlayerController : MonoBehaviour
{
    public float speed;

    public float forwardInput;
    public float horizontalInput;

    private Rigidbody rb;

    public static PlayerController instance;

    private void Awake()
    {
        PlayerData.SaveData();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        instance = this;

        Vector3 oldPos = new Vector3(PlayerPrefs.GetFloat("PlayerX", 0), PlayerPrefs.GetFloat("PlayerY", 1), PlayerPrefs.GetFloat("PlayerZ", 0));
        transform.position = oldPos;
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        Vector3 tempV = rb.velocity;

        tempV.x = horizontalInput * speed;
        tempV.z = forwardInput * speed;

        rb.velocity = tempV;

        tempV.y = 0;
        transform.LookAt(tempV.normalized + transform.position);
    }
}
