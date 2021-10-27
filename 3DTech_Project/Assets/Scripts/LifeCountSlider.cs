using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCountSlider : MonoBehaviour
{
    public Text myText;
    public Slider mySlider;
    // Start is called before the first frame update
    void Start()
    {
      DontDestroyOnLoad(PlayerStats.Instance);
    }

    // Update is called once per frame
    void Update()
    {
        myText.text = "Nombre de vies: " + mySlider.value;
        PlayerStats.Instance.Health = mySlider.value;
        PlayerStats.Instance.MaxHealth = mySlider.value;
    }
}
