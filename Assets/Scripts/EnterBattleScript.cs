using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBattleScript : MonoBehaviour
{
    TransitionHandler trans;

    // Start is called before the first frame update
    void Start()
    {
        trans = FindObjectOfType<TransitionHandler>().GetComponent<TransitionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            trans.SlideIn();
            StartCoroutine("TriggerBattle");
        }
    }

    private IEnumerator TriggerBattle()
    {
        yield return new WaitForSeconds(1.5f);

        trans.SlideOut();

        SceneManager.LoadScene("FightScene");

    }    
}
