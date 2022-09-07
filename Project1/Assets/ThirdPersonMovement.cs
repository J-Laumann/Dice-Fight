using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Name: Brian Sida
 * Assignment: Project 1
 * Description: Controls players movement in third-person
*/
public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocty;

    private void Start()
    {
        // Locks the mouse in the center of screen at the start of the game
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        // Stores vectors x and z, but not y
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Detects movement
        if (direction.magnitude >= 0.1f)
        {
            // Turns the player towards the direction they are facing
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            // Smooths the rotation the player makes when turning
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocty, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Makes the player travel in the direction the camera is facing
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

}
