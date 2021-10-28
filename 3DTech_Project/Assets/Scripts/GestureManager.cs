using System.Collections;
using System.Collections.Generic;
using PDollarGestureRecognizer;
using UnityEngine;
using UnityEngine.UI;
using Util;
using UnityEngine.SceneManagement;
public class GestureManager : MonoBehaviour
{

    public GameObject gestureItemTemplate;
    public Button addGesture;
    public Button back;
    
    private List<GameObject> gestureItemList;
    
    // Start is called before the first frame update
    void Start()
    {
        gestureItemList = new List<GameObject>();

        foreach (Gesture gesture in PDollarUtil.LoadPreMadeGesture())
        {
            AddGesture(gesture.Name);
        }

        foreach (Gesture gesture in PDollarUtil.LoadCustomGesture())
        {
            AddGesture(gesture.Name);
        }
        
        addGesture.onClick.AddListener(GoToAddGestureScene);
        back.onClick.AddListener(GoToMainMenu);
    }

    // Update is called once per frame
 

    public void AddGesture(string name)
    {
        GameObject newGesture = Instantiate(gestureItemTemplate) as GameObject;
        newGesture.SetActive(true);
        newGesture.GetComponent<ItemGesture>().SetName(name);  
        newGesture.transform.SetParent(gestureItemTemplate.transform.parent,false);
        gestureItemList.Add(newGesture);
    }

    private void GoToAddGestureScene()
    {
        SceneManager.LoadScene("AddGesture");
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
