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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
