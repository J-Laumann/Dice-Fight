using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DieSlotData
{
    public int min = 1, max = 6;
    public int fillAmount = -1;
}

public class DieSlot : MonoBehaviour
{
    public TMP_Text fillText, minText, maxText;

    public Die die;
    public DieSlotData data;
    public PlayerAbility ability;

    // Start is called before the first frame update
    void Awake()
    {
        data = new DieSlotData();

        die = transform.GetComponentInChildren<Die>();
        if (die)
        {
            die.slot = this;
            die.ogSlot = this;
        }

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        if(ability != null && ability.ability.uses == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            if (data.fillAmount > -1)
            {
                fillText.text = data.fillAmount + "";
            }
            if(data.min != 1)
            {
                minText.text = "Min: " + data.min;
            }
            if(data.max != 6)
            {
                maxText.text = "Max: " + data.max;
            }
        }
    }
}
