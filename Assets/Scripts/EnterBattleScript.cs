using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBattleScript : MonoBehaviour
{
    [Tooltip("THIS NEEDS TO BE UNIQUE")]
    public string enemyID;
    public EnemyData enemy;

    private void Awake()
    {
        if (PlayerPrefs.GetInt(enemyID + "_DEAD", 0) == 1)
        {
            Destroy(gameObject);
        }
    }

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
        if(other.CompareTag("Player") && PlayerPrefs.GetInt(enemyID + "_DEAD", 0) == 0)
        {
            TransitionHandler.instance.SlideIn();
            StartCoroutine("TriggerBattle");
        }
    }

    private IEnumerator TriggerBattle()
    {
        yield return new WaitForSeconds(1.5f);

        TransitionHandler.instance.SlideOut();

        PlayerPrefs.SetFloat("PlayerX", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", PlayerController.instance.transform.position.z);
        GameManager.enemy = enemy;
        GameManager.enemyID = enemyID;
        SceneManager.LoadScene("FightScene");

    }    
}
