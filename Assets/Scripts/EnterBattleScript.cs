using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBattleScript : MonoBehaviour
{
    public EnemyData enemy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            TransitionHandler.instance.SlideIn();
            StartCoroutine("TriggerBattle");
        }
    }

    private IEnumerator TriggerBattle()
    {
        yield return new WaitForSeconds(1.5f);

        TransitionHandler.instance.SlideOut();

        GameManager.enemy = enemy;
        SceneManager.LoadScene("FightScene");

    }    
}
