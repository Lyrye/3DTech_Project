using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGesture : MonoBehaviour
{
    public void SetName(string name)
    {
        GetComponent<Text>().text = "Caractère " + name;
    }
    
}
