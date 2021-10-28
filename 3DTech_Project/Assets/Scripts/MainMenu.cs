using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  
    public void Awake()
    {
        AudioManager.Instance.PlayAudio();
    }

    public void ExitButton()
    {
      Application.Quit();
      Debug.Log("Application quittee");
    }

    public void StartGame()
    {
      SceneManager.LoadScene("Game");
    }

    public void StartMenu()
    {
      SceneManager.LoadScene("MainMenu");
    }

    public void StartGestes()
    {
      SceneManager.LoadScene("ManageGesture");
    }
}
