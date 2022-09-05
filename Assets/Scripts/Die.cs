using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Die : MonoBehaviour
{
    public int value;
    Vector3 startPos, offset;
    public bool beingDragged;

    Image image;

    // Start is called before the first frame update
    void Start()
    {
        // I want to rollll the dieee
        image = GetComponent<Image>();
        value = Random.Range(1, 7);
        image.sprite = GameManager.instance.diceSprites[value - 1];
    }

    // Update is called once per frame
    void Update()
    {
        if (beingDragged)
        {
            transform.position = Input.mousePosition + offset;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (beingDragged)
            {
                EndDrag();
            }
        }
    }

    public void StartDrag()
    {
        offset = transform.position - Input.mousePosition;
        startPos = transform.position;
        beingDragged = true;
    }

    public void EndDrag()
    {
        // Check if over a DieSpot
        if (false)
        {

        }
        // Check if being placed back in "hand"
        else if (false)
        {

        }
        // Otherwise, snap back to before drag position
        else
        {
            transform.position = startPos;
        }

        beingDragged = false;
    }
}
