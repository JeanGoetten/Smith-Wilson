using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public ActionSceneManager sceneManager;

    private Animator anim;

    public string nextScene; 
    

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
        if (!BossController.bossIsAlive)
        {
            anim.SetTrigger("bossDeath");
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
    public void NextScene()
    {
        sceneManager.NextScene(nextScene);
    }
}
