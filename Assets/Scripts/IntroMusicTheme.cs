using System.Collections.Generic;
using UnityEngine;

public class IntroMusicTheme : MonoBehaviour
{
    public AudioSource au;
    public List<AudioClip> clips; 

    public NextScene nextScene;

    public void Play(int clip)
    {
        au.PlayOneShot(clips[clip]); 
    }

    public void NextScene()
    {
        nextScene.GoToNextScene();
    }
}
