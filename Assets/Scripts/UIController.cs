using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public ActionSceneManager sceneManager;

    private Animator anim;

    

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        
        if (!ActionSceneManager.neoIsAlive)
        {
            anim.SetTrigger("neoDeath"); 
        }
        if (!ActionSceneManager.smithSafe)
        {
            anim.SetTrigger("smithDeath");
        }
    }
    private void LateUpdate()
    {
        ActionSceneManager.smithSafe = true;
    }

    public void RestartScene()
    {
        sceneManager.RestartScene();
    }
}
