using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public enum DieType
{
    D4 = 4,
    D6 = 6,
    D8 = 8,
    D10 = 10,
    D12 = 12,
    D20 = 20
}

public class Die : MonoBehaviour
{
    public DieType type;
    public int value;
    Vector3 offset;
    public bool beingDragged;
    public DieSlot slot, ogSlot;

    Image image;
    TMP_Text valueText;

    // Start is called before the first frame update
    void Start()
    {
        // I want to rollll the dice
        image = GetComponent<Image>();
        valueText = GetComponentInChildren<TMP_Text>();
        value = UnityEngine.Random.Range(1, (int)type + 1);
        StartCoroutine(RollDie());

        image.sprite = GameManager.instance.diceSprites[Array.IndexOf(Enum.GetValues(type.GetType()), type)];
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
            if(value >= newSlot.data.min && value <= newSlot.data.max && newSlot.data.fillAmount != 0)
                MoveToSlot(newSlot);
            else
                transform.position = slot.transform.position;
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
        if (slot.data.fillAmount > 0)
        {
            slot.data.fillAmount = Mathf.Clamp(slot.data.fillAmount - value, 0, slot.data.fillAmount);
            slot.UpdateUI();

            Destroy(gameObject);
        }

        bool filled = true;
        foreach (DieSlot slot in slot.ability.dieSlots)
        {
            if (slot.data.fillAmount > 0 || (slot.data.fillAmount == -1 && slot.die == null))
            {
                filled = false;
                break;
            }
        }

        if (filled)
        {
            slot.ability.DoAbility();

            transform.position = slot.transform.position;
        }
    }

    private void OnDestroy()
    {
        Destroy(ogSlot.gameObject);
    }

    IEnumerator RollDie()
    {
        for (int i = 0; i < 10; i++)
        {
            valueText.text = "" + UnityEngine.Random.Range(1, (int)type + 1);
            yield return new WaitForSeconds(0.1f);
        }

        valueText.text = "" + value;
    }
}
