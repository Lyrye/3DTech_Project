using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{

    public Slider Volume;
    // Start is called before the first frame update
    void Start()
    {
        Volume.value = PlayerPrefs.GetFloat("Volume");
    }

    // Update is called once per frame
    void Update()
    {
        AudioManager.Instance.SetVolume(Volume.value);
    }
}
