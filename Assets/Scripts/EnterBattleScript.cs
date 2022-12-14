using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBattleScript : MonoBehaviour
{
    [Tooltip("THIS NEEDS TO BE UNIQUE")]
    public string enemyID;
    public EnemyData enemy;

    public static bool firstFight = true;

    private void Awake()
    {
        if (PlayerPrefs.GetInt(enemyID + "_DEAD", 0) == 1)
        {
            Destroy(gameObject);
        }
        else if(PlayerPrefs.GetInt(enemyID + "_DEAD", 0) == 2)
        {
            MeshRenderer mr = GetComponent<MeshRenderer>();
            mr.material.color = Color.blue;
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
        if (other.CompareTag("Player") && PlayerPrefs.GetInt(enemyID + "_DEAD", 0) == 0)
        {
            firstFight = false;
            TransitionHandler.instance.SlideIn();
            StartCoroutine("TriggerBattle");
            if (FindObjectOfType<TutorialTrigger>() != null)
                FindObjectOfType<TutorialTrigger>().TriggerOverworldTutorial();
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
