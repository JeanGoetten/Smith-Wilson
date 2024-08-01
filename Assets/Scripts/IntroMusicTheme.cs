using System.Collections.Generic;
using UnityEngine;

public class IntroMusicTheme : MonoBehaviour
{
    public AudioSource au;
    public List<AudioClip> clips; 

    public void Play(int clip)
    {
        au.PlayOneShot(clips[clip]); 
    }
}
