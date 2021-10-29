using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
      public GameObject osbtaclePrefab;                                    //The osbtacle game object.
      public int osbtaclePoolSize = 5;                                    //How many osbtacles to keep on standby.
      public float spawnRate = 3f;                                    //How quickly osbtacles spawn.
      public float osbtacleMin = -1f;                                    //Minimum y value of the osbtacle position.
      public float osbtacleMax = 3.5f;                                    //Maximum y value of the osbtacle position.

      private GameObject[] osbtacles;                                    //Collection of pooled osbtacles.
      private int currentosbtacle = 0;                                    //Index of the current osbtacle in the collection.

      private Vector2 objectPoolPosition = new Vector2 (-15,-25);        //A holding position for our unused osbtacles offscreen.
      private float spawnXPosition = 3f;

      private float timeSinceLastSpawned;


      void Start()
      {
          timeSinceLastSpawned = 0f;

          //Initialize the osbtacles collection.
          osbtacles = new GameObject[osbtaclePoolSize];
          //Loop through the collection...
          for(int i = 0; i < osbtaclePoolSize; i++)
          {
              //...and create the individual osbtacles.
              osbtacles[i] = (GameObject)Instantiate(osbtaclePrefab, objectPoolPosition, Quaternion.identity);
          }
      }


      //This spawns osbtacles as long as the game is not over.
      void Update()
      {
          timeSinceLastSpawned += Time.deltaTime;

          if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= spawnRate)
          {
              timeSinceLastSpawned = 0f;

              //Set a random y position for the osbtacle
              float spawnYPosition = Random.Range(osbtacleMin, osbtacleMax);

              //...then set the current osbtacle to that position.
              osbtacles[currentosbtacle].transform.position = new Vector2(spawnXPosition, spawnYPosition);

              //Increase the value of currentosbtacle. If the new size is too big, set it back to zero
              currentosbtacle ++;

              if (currentosbtacle >= osbtaclePoolSize)
              {
                  currentosbtacle = 0;
              }
          }
      }
}
