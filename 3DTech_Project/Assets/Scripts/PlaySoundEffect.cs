using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEffect : MonoBehaviour
{
    
    public AudioSource AudioSourceA;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlaySound(AudioClip clip)
    {
        AudioSourceA.clip = clip; 
        AudioSourceA.Play();
    }
}
