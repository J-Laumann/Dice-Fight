using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSlot : MonoBehaviour
{
    public Die die;

    // Start is called before the first frame update
    void Start()
    {
        if(transform.childCount > 0)
        {
            die = transform.GetComponentInChildren<Die>();
            die.slot = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
