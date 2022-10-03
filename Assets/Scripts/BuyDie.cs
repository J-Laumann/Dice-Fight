using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyDie : MonoBehaviour
{
    Button button;

    public int cost;
    public DieType die;

    public TMP_Text nameText, costText;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerData.pand.dice.Contains(die))
            Destroy(gameObject);

        button = GetComponent<Button>();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate { SoulShop.instance.Purchase(cost, die, gameObject); });

        nameText.text = die.ToString();
        costText.text = cost + " S";
    }
}
