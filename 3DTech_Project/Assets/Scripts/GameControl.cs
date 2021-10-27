using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public GameObject gameOverText;
    public Text scoreText;
    public bool gameOver = false;

    public float scrollSpeed = -1.5f;
    private int score = 0;                        //The player's score.

    // Start is called before the first frame update
    void Awake()
    {
      //If we don't currently have a game control...
      if (instance == null)
          //...set this one to be it...
          instance = this;
      //...otherwise...
      else if(instance != this)
      {
        //...destroy this one because it is a duplicate.
        Destroy (gameObject);
      }

        PlayerStats.Instance.Health = LifeCountSlider.lifeCount;
        PlayerStats.Instance.MaxHealth = LifeCountSlider.lifeCount;
    //    PlayerStats.Instance.Heal(100.0f);
    }

    // Update is called once per frame
    void Update()
    {
      //If the game is over and the player has pressed some input...
      /*
      if (gameOver && Input.GetMouseButtonDown(0))
      {
          //...reload the current scene.
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      }
      */
    }

    public void ChickenScored()
    {
        //The bird can't score if the game is over.
        if (gameOver)
            return;
        //If the game is not over, increase the score...
        //TODO_SCORE
        score++;
        //...and adjust the score text.
        scoreText.text = "Score: " + score.ToString();
    }

    public void ChickenDied()
    {
      //gameOverText.setActive(true);
      gameOver = true;
    }
}
