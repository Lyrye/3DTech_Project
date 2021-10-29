using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{

    public Slider Volume;
    void Start()
    {
        Volume.value = PlayerPrefs.GetFloat("Volume");
    }
    void Update()
    {
        AudioManager.Instance.SetVolume(Volume.value);
        PlayerPrefs.SetFloat("Volume",Volume.value);
    }
}
