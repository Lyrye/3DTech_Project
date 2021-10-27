using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCountSlider : MonoBehaviour
{
    public Text myText;
    public Slider mySlider;
    public static float lifeCount = 3;
    // Start is called before the first frame update
    void Start()
    {
      mySlider.value = lifeCount;
    }

    // Update is called once per frame
    void Update()
    {
        myText.text = "Nombre de vies: " + mySlider.value;
        lifeCount = mySlider.value;
      //  PlayerStats.Instance.Health = mySlider.value;
    //    PlayerStats.Instance.MaxHealth = mySlider.value;
    }
}
