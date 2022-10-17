using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyAbility : MonoBehaviour
{

    Button button;

    public int cost;
    public AbilityData ability;
    [Tooltip("Used for upgradeable abilities")]
    public AbilityData oldAbility;

    public TMP_Text nameText, costText;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerData.pand.abilities.Contains(ability))
            Destroy(gameObject);

        button = GetComponent<Button>();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate { SoulShop.instance.Purchase(cost, ability, oldAbility, gameObject); });

        nameText.text = ability.abilityName;
        costText.text = cost + " S";
    }
}
