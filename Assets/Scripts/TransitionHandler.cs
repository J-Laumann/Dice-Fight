using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionHandler : MonoBehaviour
{
    Animator anim;
    public static TransitionHandler instance;

    // Start is called before the first frame update
    void Start()
    {
        // Grabs the animator from the Transition Image.
        anim = GetComponentInChildren<Animator>();

        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// Plays the Slide In transition.
    /// </summary>
    public void SlideIn()
    {
        anim.Play("Slide_In");
    }

    /// <summary>
    /// Plays the Slide out transition.
    /// </summary>
    public void SlideOut()
    {
        anim.Play("Slide_Out");
    }
}
