using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEffect : MonoBehaviour
{
    
    public AudioSource AudioSourceA;
    public void PlaySound(AudioClip clip)
    {
        AudioSourceA.clip = clip; 
        AudioSourceA.Play();
    }
}
