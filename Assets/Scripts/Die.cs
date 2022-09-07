using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Die : MonoBehaviour
{
    public int value;
    Vector3 offset;
    public bool beingDragged;
    public DieSlot slot, ogSlot;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        // I want to rollll the dice
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
        beingDragged = true;
    }

    public void EndDrag()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        DieSlot newSlot = null;
        foreach(RaycastResult result in results)
        {
            newSlot = result.gameObject.GetComponent<DieSlot>();
            if (newSlot)
                break;
        }

        // Check if over a DieSpot
        if (newSlot)
        {
            if(value >= newSlot.data.min && value <= newSlot.data.max)
                MoveToSlot(newSlot);
        }
        // Otherwise, snap back to before drag position
        else
        {
            transform.position = slot.transform.position;
        }

        beingDragged = false;
    }

    public void MoveToSlot(DieSlot newSlot)
    {
        slot.die = newSlot.die;
        if(slot.die != null)
        {
            slot.die.slot = slot;
            slot.die.transform.position = slot.transform.position;
            slot.die.transform.parent = slot.transform;
            slot.die.ogSlot = slot;
        }

        newSlot.die = this;
        transform.position = newSlot.transform.position;
        transform.parent = newSlot.transform;
        slot = newSlot;

        if (slot.ability)
            SlotAction();
        else
            ogSlot = slot;
    }

    public void SlotAction()
    {
        if (slot.ability)
        {
            if (slot.data.fillAmount > 0)
            {
                slot.data.fillAmount = Mathf.Clamp(slot.data.fillAmount - value, 0, slot.data.fillAmount);
                slot.UpdateUI();

                if(slot.data.fillAmount == 0)
                {
                    slot.ability.DoAbility(-1);
                }

                Destroy(gameObject);
            }
            else if(slot.data.fillAmount == -1)
            {
                slot.ability.DoAbility(value);
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        Destroy(ogSlot.gameObject);
    }
}
